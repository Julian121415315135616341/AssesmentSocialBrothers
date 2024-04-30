public class AddresDTO
{

    private int id;
    private String street;
    private int number;
    private String code;
    private String city;
    private String country;



    public AddresDTO(int id, String street, int number, String code, String city, String country){
        this.id = id;
        this.street = street;
        this.number = number;
        this.code = code;
        this.city = city;
        this.country = country;
    }

    public AddresDTO(Addres adress){
        this.id = adress.getId();
        this.street = adress.getStreet();
        this.number = adress.getNumber();
        this.code = adress.getCode();
        this.city = adress.getCity();
        this.country = adress.getCountry();
    }

public int Id
    {
        get { return id; }
    }

    public int Number
    {
        get { return number; }
    }

    public string Street
    {
        get { return street; }
    }

    public string City
    {
        get { return city; }
    }

    public string Code
    {
        get { return code; }
    }

    public string Country
    {
        get { return country; }
    }

    public override string ToString()
    {
        return $"AddresDTO{{id={id}, street='{street}', number={number}, code='{code}', city='{city}', country='{country}'}}";
    }

}