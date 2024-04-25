using System;
using Microsoft.Data.Sqlite;

public class DatabaseManager{

private string connectionString;

    public DatabaseManager()
    {
        this.connectionString = @"Data Source=C:\School\Jaar 2\SocialBrotherAssesment\AssesmentSocialBrothers\WebApplication1\database.db";
;
    }

    public SqliteConnection OpenConnection()
    {
        SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection;
    }

    public void CloseConnection(SqliteConnection connection)
    {
        connection.Close();
    }
}

public class AddressManager
{
    private DatabaseManager dbManager;
    private string connectionString;

    public AddressManager(string connectionString)
    {
        this.connectionString = connectionString;
        this.dbManager = new DatabaseManager();
    }

    public void CreateNewAddress(Adress address)
    {
        using (SqliteConnection connection = dbManager.OpenConnection())
        {
            string query = "INSERT INTO Addresses (id, Street, Number, Code, City, Country) VALUES (@Street, @Number, @Code, @City, @Country)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", address.getId());
            command.Parameters.AddWithValue("@Street", address.getStreet());
            command.Parameters.AddWithValue("@Number", address.getNumber());
            command.Parameters.AddWithValue("@Code", address.getCode());
            command.Parameters.AddWithValue("@City", address.getCity());
            command.Parameters.AddWithValue("@Country", address.getCountry());
            command.ExecuteNonQuery();
        }
    }
}