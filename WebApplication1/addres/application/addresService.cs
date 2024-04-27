using System;
using Microsoft.Data.Sqlite;

public class AddresService
{

    private AddressManager addressManager;
    public AddresService(){
        this.addressManager = new AddressManager();
    }


   public AddresDTO CreateAddres(string street, int number, string code, string city, string country){
    
    try
    {
        int id = addressManager.GetAddresses().Count()+1;
        Addres addres = new Addres(id, street, number, code, city, country);
        addressManager.CreateNewAddress(addres);
        return new AddresDTO(addres);
    }
    catch (SqliteException ex)
    {
        Console.WriteLine("SQLite Exception occurred: " + ex.Message);
        throw new SqliteException(ex.Message, ex.ErrorCode); 
    }
}
public AddresDTO changeAddres(NewAddressDTO newAddressDTO, int id){
    try {
        AddresDTO addresDTO = new AddresDTO(id, newAddressDTO.Street, newAddressDTO.Number, newAddressDTO.Code, newAddressDTO.City, newAddressDTO.Country);
        return addressManager.changeAddres(addresDTO);
    }
    catch (SqliteException ex){
        Console.WriteLine("SQLite Exception occurred: " + ex.Message);
        throw new SqliteException(ex.Message, ex.ErrorCode);
    }
}

public List<AddresDTO> getAddresses(){
    return addressManager.GetAddresses();
}

public AddresDTO getAddres(int id){
    return addressManager.GetAddres(id);
}

    
}