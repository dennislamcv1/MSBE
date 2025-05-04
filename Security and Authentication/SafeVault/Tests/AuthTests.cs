using System.Data.SQLite;
using Xunit;
using FluentAssertions;

public class AuthTests
{
    private readonly AuthService _authService;

    public AuthTests()
    {
        var connection = new SQLiteConnection("Data Source=:memory:");
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"CREATE TABLE Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Email TEXT NOT NULL,
                PasswordHash TEXT NOT NULL,
                Role TEXT NOT NULL
            );";
            command.ExecuteNonQuery();
        }

        _authService = new AuthService(connection);
    }

    [Fact]
    public void Register_Should_Succeed_With_Valid_Data()
    {
        bool result = _authService.Register("user1", "user1@test.com", "Password123");
        result.Should().BeTrue();
    }

    [Fact]
    public void Login_Should_Return_Null_For_Wrong_Password()
    {
        _authService.Register("user2", "user2@test.com", "CorrectPassword");
        var user = _authService.Login("user2", "WrongPassword");
        user.Should().BeNull();
    }

    [Fact]
    public void Login_Should_Return_User_If_Credentials_Valid()
    {
        _authService.Register("user3", "user3@test.com", "MySecret");
        var user = _authService.Login("user3", "MySecret");
        user.Should().NotBeNull();
        user!.Username.Should().Be("user3");
    }
}
