var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddTransient<AddresService>(); // Assuming AddressService is your service class

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/addresses", (AddresService addressService) => {
    return addressService.getAddresses();
})
.WithName("GetAddresses")
.WithOpenApi(); // Assuming AddressService has a method GetAddresses()

app.Run();