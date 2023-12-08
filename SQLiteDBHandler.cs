
using System;
using System.Data.SQLite;

public class SQLiteDBHandler
{
   private string _connectionString;

    // Aggiungi una proprietà pubblica per ottenere la stringa di connessione
    public string ConnectionString
    {
        get { return _connectionString; }
    }

    public SQLiteDBHandler(string dbFileName)
    {
        _connectionString = $"Data Source={dbFileName};Version=3;";
        InitializeDatabase();
    }
   


    private void InitializeDatabase()
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string tableCreationQuery = @"
                CREATE TABLE IF NOT EXISTS LogEntries (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CallSign TEXT NOT NULL,
                    Band TEXT NOT NULL,
                    Mode TEXT NOT NULL,
                    QsoDate TEXT NOT NULL,
                    ReportSent TEXT,
                    ReportReceived TEXT
                )";
                using (SQLiteCommand command = new SQLiteCommand(tableCreationQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SQLiteException e)
        {
            Console.WriteLine("Errore durante l'inizializzazione del database: " + e.Message);
            // Qui puoi anche aggiungere un logging degli errori o altre azioni
        }
    }

    public void SaveLogEntry(LogEntry entry)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO LogEntries (CallSign, Band, Mode, QsoDate, ReportSent, ReportReceived) VALUES (@CallSign, @Band, @Mode, @QsoDate, @ReportSent, @ReportReceived)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@CallSign", entry.CallSign);
                    command.Parameters.AddWithValue("@Band", entry.Band);
                    command.Parameters.AddWithValue("@Mode", entry.Mode);
                    command.Parameters.AddWithValue("@QsoDate", entry.QsoDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@ReportSent", entry.ReportSent);
                    command.Parameters.AddWithValue("@ReportReceived", entry.ReportReceived);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (SQLiteException e)
        {
            Console.WriteLine("Errore durante il salvataggio di una voce di log: " + e.Message);
            // Anche qui puoi aggiungere un logging degli errori o altre azioni
        }
    }

    // Qui puoi aggiungere ulteriori metodi come Update, Delete, Read ecc.

    // Metodo privato per ottenere una connessione SQLite aperta (per ridurre la duplicazione del codice)
    private SQLiteConnection GetOpenConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
