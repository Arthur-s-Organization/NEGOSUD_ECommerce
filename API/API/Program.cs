using API.Data;
using API.Models;
using API.Services;
using API.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//	.AddEntityFrameworkStores<DataContext>();


// Add services to the container.


builder.Services.AddIdentity<Customer, IdentityRole>()
	.AddEntityFrameworkStores<DataContext>()
	.AddDefaultTokenProviders();




builder.Services.Configure<IdentityOptions>(options =>
{
	options.SignIn.RequireConfirmedEmail = false;
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// Ajout de la mise en cache des sessions
builder.Services.AddDistributedMemoryCache(); // Nécessaire pour stocker les sessions en mémoire

// Configuration des options de session
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée d'inactivité avant expiration
	options.Cookie.HttpOnly = true; // La session est accessible uniquement par le serveur
	options.Cookie.IsEssential = true; // Rendre le cookie essentiel pour les fonctionnalités de l'application
});


//Gestion des CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3002", "http://client:3000", "http://10.5.1.12:3000", "http://egotestv10.duhamel-logisitque.fr")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
}); ;
//

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAlcoholFamilyService, AlcoholFamilyService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<ISupplierOrderService, SupplierOrderService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISessionService, SessionService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.MapIdentityApi<Customer>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseSession(); // Activer la gestion des sessions


app.UseAuthentication();
app.UseAuthorization();
app.UseCors();


app.MapControllers();

app.Run();
