using System.Reflection;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register services
builder.Services.AddTransient<AddresService>(); 

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/addresses", (AddresService addresService, [FromQuery] string search = null, [FromQuery] string sortField = null, [FromQuery] string sortOrder = null) => {
    var addresses = addresService.getAddresses();

    if (!string.IsNullOrEmpty(search))
    {
        addresses = addresses.Where(a =>
            a.Street.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            a.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            a.Code.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            a.Country.Contains(search, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
    
    if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
    {
        var prop = typeof(AddresDTO).GetProperty(sortField, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (prop != null)
        {
            if (sortOrder.ToLower() == "asc")
            {
                addresses = addresses.OrderBy(a => prop.GetValue(a, null)).ToList();
            }
            else
            {
                addresses = addresses.OrderByDescending(a => prop.GetValue(a, null)).ToList();
            }
        }
    }

    return new JsonResult(addresses);
})
.WithName("GetAddresses")
.WithOpenApi();

app.MapGet("/addresses/{id}", (AddresService addresService, int id) => {
    var address = addresService.getAddres(id);
    if (address != null)
    {
        return new JsonResult(address);
    }
    else
    {
        return new JsonResult($"Address with ID {id} not found."); 
    }
})
.WithName("GetAddressById")
.WithOpenApi();

app.MapPut("/addresses/{id}", async (HttpContext httpContext, int id) => {
        var addresService = httpContext.RequestServices.GetRequiredService<AddresService>();
        var newAddres = await httpContext.Request.ReadFromJsonAsync<NewAddressDTO>();

        if (newAddres != null && newAddres.City != null && newAddres.Code != null && newAddres.Country != null && newAddres.Street != null && newAddres.Number != null && newAddres.Number != 0){
                    AddresDTO adresDTO = addresService.changeAddres(newAddres, id);
            return new JsonResult(adresDTO);
        }
        else {
            return new JsonResult("Addres not found");
        }
})
.WithName("ChangeAddres")
.WithOpenApi();

app.MapPost("/addresses", async (HttpContext httpContext) => {

    var addresService = httpContext.RequestServices.GetRequiredService<AddresService>();
    var newAddres = await httpContext.Request.ReadFromJsonAsync<NewAddressDTO>();
    if (newAddres != null && newAddres.City != null && newAddres.Code != null && newAddres.Country != null && newAddres.Street != null && newAddres.Number != null && newAddres.Number != 0)
    {
        AddresDTO createdAddres = addresService.CreateAddres(newAddres.Street, newAddres.Number, newAddres.Code, newAddres.City, newAddres.Country);

        return new JsonResult(createdAddres);
    }
    else
    {
        return new JsonResult("Invalid JSON body"); 
    }
})
.WithName("CreateAddress")
.WithOpenApi();


app.MapDelete("/addresses/{id}", async (HttpContext httpContext, int id) => {

    var addresService = httpContext.RequestServices.GetRequiredService<AddresService>();
    if (addresService.deleteAddres(id)){
        return new JsonResult("Addres succesfully deleted");
    }
    else {
        return new JsonResult("Addres not found");
    }
})
.WithName("DeleteAddres")
.WithOpenApi();


app.Run();