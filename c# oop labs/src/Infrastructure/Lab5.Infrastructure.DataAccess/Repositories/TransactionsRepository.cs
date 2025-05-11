using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionsRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public ICollection<string>? ShowTransactions(long userID)
    {
        const string sql = """
        select user_id, transaction
        from Transactions
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

        var transactions = new List<string>();

        for (ulong i = 0; i < reader.Rows; ++i)
        {
            transactions.Add(reader.GetString((int)i));
        }

        return transactions;
    }
}