using System.Globalization;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker;

internal static class CodingController
{
    internal static void StartSession()
    {
        var Start = AnsiConsole.Ask<string>("\n\nType [green]'Start'[/] to start the logger!");

        string Date = null;
        if (Start.ToLower() == "start")
        {
            Date = DateTime.Now.ToString();
            AnsiConsole.Write(new Markup("[bold red]Type 'End' to end the current session. [/]"));
        }
        else if (Start == "0")
        {
            Menu.MenuScreen();
        }
        else
        {
            AnsiConsole.Write(new Markup("[bold red]Invalid Input Entered, Try Again![/]"));
            StartSession();
        }

        string End = Console.ReadLine();
        string EndDate;

        if (End.ToLower() == "end")
        {
            EndDate = DateTime.Now.ToString();
            TimeSpan Duration = DateTime.Parse(EndDate) - DateTime.Parse(Date);
            string DurationDB = Duration.ToString();
            using (var connection = new SqliteConnection(Model.ConnectionString))
            {
                connection.Open();
                var tablecmd = connection.CreateCommand();

                tablecmd.CommandText =
                    $"INSERT INTO coding_logger(StartTime, EndTime, Duration) VALUES ('{Date}', '{EndDate}', '{DurationDB}')";

                tablecmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        else if (Start == "0")
        {
            Menu.MenuScreen();
        }
        else
        {
            AnsiConsole.Write(new Markup("[bold red]Invalid Input Entered, Try Again![/]"));
            StartSession();
        }
    }

    internal static void ViewSessions()
    {
        using (var connection = new SqliteConnection(Model.ConnectionString))
        {
            connection.Open();
            var tablecmd = connection.CreateCommand();

            tablecmd.CommandText = $"SELECT * FROM coding_logger";

            List<CodingLogger> data = new();

            SqliteDataReader reader = tablecmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    data.Add(
                        new CodingLogger
                        {
                            Id = reader.GetInt32(0),
                            StartTime = DateTime.ParseExact(reader.GetString(1), "M/d/yyyy h:mm:ss tt",
                                CultureInfo.InvariantCulture),
                            EndTime = DateTime.ParseExact(reader.GetString(2), "M/d/yyyy h:mm:ss tt",
                                CultureInfo.InvariantCulture),
                            Duration = DateTime.ParseExact(reader.GetString(3), "hh\\:mm\\:ss",
                                CultureInfo.InvariantCulture),
                        });
                }
            }
            else
            {
                AnsiConsole.Write(new Markup(
                    "[bold red]No rows found in the database. Please use the coding logger first before using this command![/]"));
            }

            connection.Close();
            AnsiConsole.Write(new Markup("[bold yellow]\nHere is the list of sessions recorded.\n[/]"));
            //Spectre Table Setup: 
            var table = new Table();
            table.AddColumn(new TableColumn("ID").Centered());
            table.AddColumn(new TableColumn("Start Time"));
            table.AddColumn(new TableColumn("End Time"));
            table.AddColumn(new TableColumn("Duration of the session"));
            table.Border(TableBorder.HeavyEdge);

            foreach (var Variable in data)
            {
                string durationFormatted =
                    $"{(int)Variable.Duration.Hour} hours {Variable.Duration.Minute} minutes {Variable.Duration.Second} seconds";
                table.AddRow(Variable.Id.ToString(), Variable.StartTime.ToString("M/d/yyyy h:mm:ss tt"),
                    Variable.EndTime.ToString("M/d/yyyy h:mm:ss tt"), durationFormatted);
            }

            AnsiConsole.Write(table);
        }

        Console.ReadKey();
    }

    internal static void DeleteSessions()
    {
        ViewSessions();

        AnsiConsole.Write(new Markup("[bold red]\nEnter the ID number of the session you would like to delete. [/]"));
        var RecordId = Console.ReadLine();

        Func<bool> confirm = () =>
        {
            if (!AnsiConsole.Confirm("[bold red]Are you sure? This action cannot be reversed![/]"))
            {
                AnsiConsole.MarkupLine("Ok... :(");
                return false;
            }

            return true;
        };

        if (!confirm())
        {
            Menu.MenuScreen();
        }
        else
        {
            using (var connection = new SqliteConnection(Model.ConnectionString))
            {
                connection.Open();
                var tablecmd = connection.CreateCommand();

                tablecmd.CommandText = $"DELETE FROM coding_logger WHERE Id = '{RecordId}'";

                int rowCount = tablecmd.ExecuteNonQuery();
                if (rowCount == 0)
                {
                    Console.WriteLine($"The specified record {RecordId} doesnt exist.");
                    DeleteSessions();
                }

                connection.Close();
            }

            Console.WriteLine($"The specified recordId {RecordId} was successfully deleted.");

            Console.ReadKey();
        }
    }
}

internal class CodingLogger
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime Duration { get; set; }
}