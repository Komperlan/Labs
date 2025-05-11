using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    create type user_role as enum
    (
        'admin',
        'employee'
    );

    create type transaction_type as enum
    (
        'ShowScore',
        'ShowTransactions', 
        'AddScore',
        'RemoveScore',
        'AddUser',
        'Login'
    );

    create table users
    (
        user_id bigint primary key generated always as identity ,
        user_name text not null ,
        user_role user_role not null ,
        user_score bigint not null ,
        user_password bigint not null 
    );

    create table Transactions
    (
        user_id bigint primary key generated always as identity ,
        transaction transaction_type not null
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table users;
    drop table Transactions;
    drop type user_role;
    drop type transaction_type;
    """;
}