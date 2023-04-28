using Dapr.Client;
using Dapr.Extensions.Configuration;
using DojoService.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add dapr secret store to the app config
// builder.Configuration.AddDaprSecretStore("localsecretstore", new DaprClientBuilder().Build());


// Get secrets using the dapr sdk
const string DAPR_SECRET_STORE = "localsecretstore";
const string SECRET_NAME = "mongodb";
var client = new DaprClientBuilder().Build();

// Get secret from a local secret store
var secrets = await client.GetBulkSecretAsync(DAPR_SECRET_STORE);
// TODO: Figure out how you want the secrets to work with the app
// grab the secret when its needed from the api, or bulk get and store in an object
// Console.WriteLine($"Fetched Secret: {secret["mongodb"]}");

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
