using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ShopBL;
using ShopDL;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddScoped<ICustomerRepo>(repo => new CustomerRepository(builder.Configuration.GetConnectionString("ShopAppDB")));
builder.Services.AddScoped<IStoreRepo>(repo => new StoreRepository(builder.Configuration.GetConnectionString("ShopAppDB")));
builder.Services.AddScoped<IProductRepo>(repo => new ProductRepository(builder.Configuration.GetConnectionString("ShopAppDB")));
builder.Services.AddScoped<IOrderRepo>(repo => new OrderRepository(builder.Configuration.GetConnectionString("ShopAppDB")));

builder.Services.AddScoped<ICustomers, Customers>();
builder.Services.AddScoped<IStores, Stores>();
builder.Services.AddScoped<IProducts, Products>();
builder.Services.AddScoped<IOrders, Orders>();

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
