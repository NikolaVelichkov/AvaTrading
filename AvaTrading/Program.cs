using AvaTrading.Data;
using AvaTrading.Entities;
using AvaTrading.Repositories;
using AvaTrading.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<NewsBackgroundService>();
builder.Services.AddHttpClient();

builder.Services.Configure<TradingNewsDatabaseSettings>(
builder.Configuration.GetSection("TradingNewsDatabase"));

var connectionString = builder.Configuration.GetConnectionString("UserAuthenticationContextConnection") ?? throw new InvalidOperationException("Connection string 'ShoppingListContextConnection' not found.");

builder.Services.AddDbContext<NewsContext>(options => {
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
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

builder.Services.AddSingleton<ITradingNewsService, TradingNewsService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Get an instance of NewsBackgroundService from the scoped service provider
var newsBackgroundService = builder.Services.BuildServiceProvider().GetRequiredService<NewsBackgroundService>();

// Register the NewsBackgroundService as a hosted service
builder.Services.AddSingleton<IHostedService>(newsBackgroundService);

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
