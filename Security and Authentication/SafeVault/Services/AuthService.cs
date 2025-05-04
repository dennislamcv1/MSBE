using Dapper;
using System.Data;
using BCrypt.Net;

public class AuthService
{
    private readonly IDbConnection _db;

    public AuthService(IDbConnection db)
    {
        _db = db;
    }

    public bool Register(string username, string email, string password)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        string sql = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, @Role)";

        var affected = _db.Execute(sql, new
        {
            Username = username,
            Email = email,
            PasswordHash = hash,
            Role = "User"
        });

        return affected == 1;
    }

    public User? Login(string username, string password)
    {
        string sql = "SELECT * FROM Users WHERE Username = @Username";
        var user = _db.QueryFirstOrDefault<User>(sql, new { Username = username });

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return user;
    }
}
