using System;
using Microsoft.Data.Sqlite;

public class AddresService
{

    private AddressManager addressManager;
    public AddresService(){
        this.addressManager = new AddressManager();
    }


   public AddresDTO CreateAddres(string street, int number, string code, string city, string country)
{
    Addres addres = new Addres(street, number, code, city, country);
    try
    {
        addressManager.CreateNewAddress(addres);
        return new AddresDTO(addres);
    }
    catch (SqliteException ex)
    {
        Console.WriteLine("SQLite Exception occurred: " + ex.Message);
        throw new SqliteException(ex.Message, ex.ErrorCode); 
    }
}

public List<AddresDTO> getAddresses(){
    return addressManager.GetAddresses();
}

    
}