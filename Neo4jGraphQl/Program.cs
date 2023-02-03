using Microsoft.Extensions.Configuration;
using Neo4j.Driver;
using Neo4jGraphQl;
using Neo4jGraphQl.Business;
using Neo4jGraphQl.Provider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application setting using IOption provider mechanism
builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

// Fetch settings object from configuration
var settings = new ApplicationSettings();
builder.Configuration.GetSection("ApplicationSettings").Bind(settings);

// This is to register your Neo4j Driver Object as a singleton
builder.Services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));

// This is your Data Access Wrapper over Neo4j session, that is a helper class for executing parameterized Neo4j Cypher queries in Transactions
builder.Services.AddScoped<INeo4jDataAccess, Neo4jDataAccess>();

// This is the registration for your domain repository class
builder.Services.AddTransient<IPersonRepository, PersonRepository>();


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
