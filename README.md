# Coding Logger / Tracker

This simple coding logger application in C# allows users to track their coding sessions by recording start time, end time, and duration. It utilizes SQLite for data storage and Spectre.Console for command-line interaction.

I made this application to practice my C# skills. In case anyone wants to use this app to track their sessions, I would recommend starting a session and letting it run in the background.

## Installation

To use this application, follow these steps:

1. Clone the repository to your local machine.
2. Ensure you have [.NET Core SDK](https://dotnet.microsoft.com/download) installed.
3. Build the solution using Visual Studio or the `dotnet build` command.
4. Run the application.

## Components

### Model.cs

The `Model.cs` file contains the model class and database setup logic. It establishes a SQLite database and creates a table to store coding session data.

### Menu.cs

The `Menu.cs` file manages the application menu and user interaction. It presents options to start tracking a session, view existing sessions, delete sessions, or exit the program.

### CodingController.cs

The `CodingController.cs` file controls the core functionality of the application. It includes methods to start a coding session, view recorded sessions, and delete sessions.

### App.config

The `App.config` file stores application settings, including the connection string for the SQLite database.

## Usage

Upon running the application, users are presented with a menu interface where they can choose various options:

- **Start Logger**: Initiates a new coding session.
- **View Sessions**: Displays a list of recorded coding sessions.
- **Delete Sessions**: Allows users to delete specific coding sessions.
- **Exit**: Terminates the application.

## Dependencies

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Spectre.Console](https://github.com/spectresystems/spectre.console)
- [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite)

---

For detailed usage instructions and code examples, refer to the source files in the repository. Thank you for using the Coding Logger Application!
