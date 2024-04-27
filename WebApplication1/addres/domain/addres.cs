using System;


public class Addres{
    private int id;
    private String street;
    private int number;
    private String code;
    private String city;
    private String country;



    public Addres(int id, String street, int number, String code, String city, String country){
        this.id = id;
        this.street = street;
        this.number = number;
        this.code = code;
        this.city = city;
        this.country = country;
    }


    public int getId(){
        return this.id;
    }
    public int getNumber(){
        return this.number;
    }


    public String getStreet(){
        return this.street;
    }
    
    public String getCity(){
        return this.city;
    }
    
    public String getCode(){
        return this.code;
    }
    
    public String getCountry(){
        return this.country;
    }

}