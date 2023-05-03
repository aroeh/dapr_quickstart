using Dapr.Client;
using Dapr.Extensions.Configuration;
using DojoService.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add dapr secret store to the app config
// Inject IConfiguration to a class to reference the items
builder.Configuration.AddDaprSecretStore("localsecretstore", new DaprClientBuilder().Build());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDojoRepo, DojoRepo>();

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
