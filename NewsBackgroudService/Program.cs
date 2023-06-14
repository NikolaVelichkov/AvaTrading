using AvaTrading.Services;
using NewsBackgroudService.Services.Contracts;
using NewsBackgroudService.Services;
using NewsBackgroudService.Settings;

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

builder.Services.AddSingleton<ITradingNewsService, TradingNewsService>();
//builder.Services.AddSingleton<IConfiguration>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
