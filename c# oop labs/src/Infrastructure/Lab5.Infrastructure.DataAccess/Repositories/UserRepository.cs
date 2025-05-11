using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Users;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? FindUserByUsername(string username)
    {
        const string sql = """
        select user_id, user_name, user_role, user_score, user_password
        from users
        where user_name = :username;
        """;

        ValueTask<NpgsqlConnection> connection = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection? connect = null;

        if (connection.IsCompleted)
        {
            connect = connection.GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connect)
            .AddParameter("username", username);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            Id: reader.GetInt64(0),
            Username: reader.GetString(1),
            Role: reader.GetFieldValue<UserRole>(2),
            Score: reader.GetInt64(3),
            Password: reader.GetInt64(4));
    }

    public User? FindUserByUserID(long userID)
    {
        const string sql = """
        select user_id, user_name, user_role, user_score, user_password
        from users
        where user_id = :userID;
        """;

        ValueTask<NpgsqlConnection> connection = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection? connect = null;

        if (connection.IsCompleted)
        {
            connect = connection.GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connect)
            .AddParameter("userID", userID);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            Id: reader.GetInt64(0),
            Username: reader.GetString(1),
            Role: reader.GetFieldValue<UserRole>(2),
            Score: reader.GetInt64(3),
            Password: reader.GetInt64(4));
    }

    public void UpdateUserScore(long score, long userID)
    {
        const string sql = """
        UPDATE users
        SET user_score = :userScore
        where user_id = :userID;
        """;

        ValueTask<NpgsqlConnection> connection = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection? connect = null;

        if (connection.IsCompleted)
        {
            connect = connection.GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connect)
            .AddParameter("userScore", score).AddParameter("userID", userID);

        using NpgsqlDataReader reader = command.ExecuteReader();
    }

    public User? CreateUser(string username, long password)
    {
        const string sql = """
        INSERT INTO users 
        (user_name, user_role, user_score, user_password) 
        VALUES(:username, 'employee', 0, :password) RETURNING user_id;
        """;

        ValueTask<NpgsqlConnection> connection = _connectionProvider
            .GetConnectionAsync(default);

        NpgsqlConnection? connect = null;

        if (connection.IsCompleted)
        {
            connect = connection.GetAwaiter().GetResult();
        }

        using NpgsqlCommand command = new NpgsqlCommand(sql, connect)
            .AddParameter("username", username).AddParameter("password", password);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            Id: reader.GetInt64(0),
            Username: username,
            Role: UserRole.Employee,
            Password: password,
            Score: 0);
    }
}