using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;
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
            string query = "INSERT INTO Addres (Street, Number, Code, City, Country) VALUES (@Street, @Number, @Code, @City, @Country)";
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
            string query = "SELECT * FROM Addres";
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

    public AddresDTO changeAddres(AddresDTO updatedAddress)
{
    using (SqliteConnection connection = dbManager.OpenConnection())
    {
        string query = "UPDATE Addres SET Street = @Street, Number = @Number, Code = @Code, City = @City, Country = @Country WHERE Id = @Id";
        SqliteCommand command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Street", updatedAddress.Street);
        command.Parameters.AddWithValue("@Number", updatedAddress.Number);
        command.Parameters.AddWithValue("@Code", updatedAddress.Code);
        command.Parameters.AddWithValue("@City", updatedAddress.City);
        command.Parameters.AddWithValue("@Country", updatedAddress.Country);
        command.Parameters.AddWithValue("@Id", updatedAddress.Id);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return updatedAddress;
        }
        else
        {
            return null;
        }
    }

}
    public Boolean deleteAddres(int id) {
    using (SqliteConnection connection = dbManager.OpenConnection())
    {
        string query = "DELETE FROM Addres WHERE Id = @Id";
        SqliteCommand command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
            {
            return true;            
            }
        else
            {
            return false;
            }
        }
    }

    public AddresDTO GetAddres(int id)
{
    using (SqliteConnection connection = dbManager.OpenConnection())
    {
        string query = "SELECT * FROM Addres WHERE id = @id";
        SqliteCommand command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                int addressId = reader.GetInt16(0);
                string street = reader.GetString(1);
                int number = reader.GetInt16(2);
                string code = reader.GetString(3);
                string city = reader.GetString(4);
                string country = reader.GetString(5);

                return new AddresDTO(addressId, street, number, code, city, country);
            }
        }
    }
    return null;   
     }
}