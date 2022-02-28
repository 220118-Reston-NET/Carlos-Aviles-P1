
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Repository;
using ShopBL;
using ShopDL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        CamelCaseText = true
    });
    var converter = new StringEnumConverter(namingStrategy: new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy());
	o.SerializerSettings.Converters.Add(converter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJWTAuthManager, JWTAuthManager>();
builder.Services.AddSwaggerGen(option => {
    option.EnableAnnotations();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtAuth:Issuer"],
        ValidAudience = builder.Configuration["JwtAuth:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Key"]))
    }
);
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
