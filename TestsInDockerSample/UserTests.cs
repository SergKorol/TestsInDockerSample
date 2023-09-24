using System.Data.Common;
using Xunit;

namespace TestsInDockerSample;

public sealed class UserTests : IClassFixture<DbFixture>, IDisposable
{
    private readonly DbConnection _dbConnection;

    public UserTests(DbFixture db)
    {
        _dbConnection = db.DbConnection;
        _dbConnection.Open();
    }

    public void Dispose()
    {
        _dbConnection.Dispose();
    }

    [Fact]
    public void UsersTableContainsJohnDoe()
    {
        // Arrange
        using var command = _dbConnection.CreateCommand();
        command.CommandText = "SELECT username, email, age FROM users;";

        // Act
        using var dataReader = command.ExecuteReader();

        // Assert
        Assert.True(dataReader.Read());
        Assert.Equal("john_doe", dataReader.GetString(0));
        Assert.Equal("john@example.com", dataReader.GetString(1));
        Assert.Equal(30, dataReader.GetInt32(2));
    }
}