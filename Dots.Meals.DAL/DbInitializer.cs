using DbUp;
using DbUp.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using SQLitePCL; // Add this

namespace Dots.Meals.DAL;
public class DbInitializer(string connString)
{
    private readonly string _connectionString = connString;

    public void Initialize()
    {
        var dbConnectionStringBuilder = new DbConnectionStringBuilder { ConnectionString = _connectionString };
        if (!dbConnectionStringBuilder.TryGetValue("Data Source", out var databaseFileObj) || databaseFileObj is not string databaseFile)
        {
            throw new InvalidOperationException("Invalid connection string: Missing 'Data Source'");
        }
        if (!File.Exists(databaseFile))
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "PRAGMA journal_mode = WAL;"; // Optional: Enable write-ahead logging
            command.ExecuteNonQuery();

            Console.WriteLine("✅ Database file created: " + databaseFile);
        }
        var upgrader = DeployChanges.To
            .SqliteDatabase(_connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(DbInitializer).Assembly)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            return;
        }

        Console.WriteLine("✅ Database migration successful!");
    }
}
