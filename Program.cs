using System;
using System.Data.SQLite;

public class UsageExample
{
    public static void RunExample()
    {
        string connectionString = "Data Source=myDatabase.sqlite;Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Creazione della tabella
            string sql = "create table if not exists sample_table (id int, value varchar(100))";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            // Inserimento di dati
            sql = "insert into sample_table (id, value) values (1, 'Esempio di Dato')";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }

            // Lettura dei dati
            sql = "select * from sample_table";
            using (var command = new SQLiteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID: " + reader["id"] + ", Valore: " + reader["value"]);
                }
            }

            connection.Close();
        }
    }
}
