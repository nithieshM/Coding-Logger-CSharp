using Spectre.Console;

namespace CodingTracker;

internal static class Menu
{
    internal static void MenuScreen()
    {
        Console.Clear();
        bool isValid = true;

        while (isValid = true)
        {
            AnsiConsole.Write(new Markup("[bold yellow]Welcome to my[/] [red]Coding Logger![/]\n"));

            var table = new Table();
            table.AddColumn(new TableColumn("Choose One of the following options!").Centered());
            table.AddRow(new Markup("[violet]1. Start Logger[/]"));
            table.AddRow(new Markup("[green]2. View Sessions[/]"));
            table.AddRow(new Markup("[blue]3. Delete Sessions[/]"));
            table.AddRow(new Markup("[bold red]Type 0 to exit the program.[/]\n"));
            table.Border(TableBorder.Rounded);
            table.HideFooters();
            AnsiConsole.Write(table);

            string InputCommand = Console.ReadLine();

            switch (InputCommand)
            {
                case "0":
                {
                    isValid = false;
                    Environment.Exit(0);
                    break;
                }

                case "1":
                {
                    CodingController.StartSession();
                    break;
                }

                case "2":
                {
                    CodingController.ViewSessions();
                    break;
                }

                case "3":
                {
                    CodingController.DeleteSessions();
                    break;
                }

                default:
                {
                    Console.WriteLine("Invalid Option, Please Try Again.");
                    break;
                }
            }
        }
    }
}