using System;
using Dapper;
using System.Data.SQLite;
using Xunit;
using FluentAssertions;

public class SqlInjectionTests
{
    [Fact]
    public void Should_Not_Allow_SQL_Injection()
    {
        using var connection = new SQLiteConnection("Data Source=:memory:");
        connection.Open();
        connection.Execute("CREATE TABLE Users (Id INTEGER PRIMARY KEY, Username TEXT, Email TEXT, PasswordHash TEXT);");

        string maliciousInput = "'; DROP TABLE Users; --";
        var sql = "INSERT INTO Users (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";
        
        Action act = () => connection.Execute(sql, new {
            Username = maliciousInput,
            Email = "attacker@example.com",
            PasswordHash = "dummyhash"
        });

        act.Should().NotThrow();

        var result = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Users';");
        result.Should().ContainSingle("Users");
    }

    private readonly AuthService _authService;

    public SqlInjectionTests()
    {
        var connection = new SQLiteConnection("Data Source=:memory:");
        connection.Open();
        connection.Execute(@"CREATE TABLE Users (
            UserID INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL,
            Email TEXT NOT NULL,
            PasswordHash TEXT NOT NULL,
            Role TEXT NOT NULL
        );");

        var password = BCrypt.Net.BCrypt.HashPassword("realpassword");
        connection.Execute("INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@u, @e, @p, @r)",
            new { u = "admin", e = "admin@test.com", p = password, r = "Admin" });

        _authService = new AuthService(connection);
    }

    [Theory]
    [InlineData("' OR '1'='1", "anything")]
    [InlineData("admin' --", "irrelevant")]
    [InlineData("admin", "' OR '1'='1")]
    public void Login_Should_Reject_SQL_Injection_Attempts(string username, string password)
    {
        var user = _authService.Login(username, password);
        user.Should().BeNull(); // No login should succeed
    }


}
