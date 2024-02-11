using System.Configuration;
using Microsoft.Data.Sqlite;

namespace CodingTracker;

static internal class Model
{
    internal static string ConnectionString = ConfigurationManager.ConnectionStrings["CodingLogger"].ConnectionString;

    static void Main(string[] args)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var tablecmd = connection.CreateCommand();

            tablecmd.CommandText = @"CREATE TABLE IF NOT EXISTS coding_logger (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    StartTime TEXT,
                                    EndTime TEXT,
                                    Duration INTEGER
            )";
            tablecmd.ExecuteNonQuery();
            connection.Close();
        }

        Menu.MenuScreen();
    }
}