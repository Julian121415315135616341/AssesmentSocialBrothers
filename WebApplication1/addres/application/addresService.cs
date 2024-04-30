using System;
using System.Text.Json;
using System.Text.Json.Nodes;
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

public Boolean deleteAddres(int id){
    return addressManager.deleteAddres(id);
}

public async Task<double?> calculateDistanceBetweenAddresses(int addresA, int addresB)
{
    {

        AddresDTO addresADTO = getAddres(addresA);
        AddresDTO addresBDTO = getAddres(addresB);
        string apiKey = "ViuTXEo8vbFi2BfBCsUnJbxeuQU8OKNaZ4x28Sa4GBXhOyoUrxAaxZKfclohrpNo"; 
        string fullAddresA = $"{addresADTO.Street} {addresADTO.Number}, {addresADTO.City}, {addresADTO.Code}, {addresADTO.Country}";
        string fullAddresB = $"{addresBDTO.Street} {addresBDTO.Number}, {addresBDTO.City}, {addresBDTO.Code}, {addresBDTO.Country}";

        string apiUrl = $"https://api.distancematrix.ai/maps/api/distancematrix/json?key={apiKey}&origins={Uri.EscapeDataString(fullAddresA)}&destinations={Uri.EscapeDataString(fullAddresB)}";

        using var client = new HttpClient();

        HttpResponseMessage response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();

            var jsonDocument = JsonDocument.Parse(responseContent);

            double distance = jsonDocument.RootElement.GetProperty("rows")[0]
                .GetProperty("elements")[0]
                .GetProperty("distance")
                .GetProperty("value")
                .GetDouble() / 1000; 
            return distance;
        }
        else
        {
            return null;
        }
    }
}
    
}