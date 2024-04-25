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

    public AddressManager()
    {
        this.dbManager = new DatabaseManager();
    }

    public void CreateNewAddress(Addres address)
    {
        using (SqliteConnection connection = dbManager.OpenConnection())
        {
            string query = "INSERT INTO Addresses (Street, Number, Code, City, Country) VALUES (@Street, @Number, @Code, @City, @Country)";
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Street", address.getStreet());
            command.Parameters.AddWithValue("@Number", address.getNumber());
            command.Parameters.AddWithValue("@Code", address.getCode());
            command.Parameters.AddWithValue("@City", address.getCity());
            command.Parameters.AddWithValue("@Country", address.getCountry());
            command.ExecuteNonQuery();
        }
    }

     public List<AddresDTO> GetAddresses()
    {
        List<AddresDTO> addresses = new List<AddresDTO>();

        using (SqliteConnection connection = dbManager.OpenConnection())
        {
            string query = "SELECT * FROM Adress";
            SqliteCommand command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt16(0);
                    string street = reader.GetString(1);
                    int number = reader.GetInt16(2);
                    string code = reader.GetString(3);
                    string city = reader.GetString(4);
                    string country = reader.GetString(5);

                    AddresDTO addres = new AddresDTO(id, street, number, code, city, country);
                    addresses.Add(addres);
                }
            }
        }

        return addresses;
    }
}