using ShopBL;
using ShopDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepo>(repo => new CustomerRepository(builder.Configuration.GetConnectionString("ShopAppDB")));
builder.Services.AddScoped<IStoreRepo>(repo => new StoreRepository(builder.Configuration.GetConnectionString("ShopAppDB")));
builder.Services.AddScoped<IProductRepo>(repo => new ProductRepository(builder.Configuration.GetConnectionString("ShopAppDB")));

builder.Services.AddScoped<ICustomers, Customers>();
builder.Services.AddScoped<IStores, Stores>();
builder.Services.AddScoped<IProducts, Products>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
