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

app.MapGet("/addresses", (AddresService addresService) => {
    return new JsonResult(addresService.getAddresses());
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

        AddresDTO adresDTO = addresService.changeAddres(newAddres, id);
        if (adresDTO != null){
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

    if (newAddres != null)
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


app.Run();