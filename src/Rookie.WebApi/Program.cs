using Rookie.Application;
using Rookie.Infrastructure;
using Rookie.Persistence;
using Rookie.WebApi;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //init database
    await app.InitializeDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.AddApi();

app.UseHttpsRedirection();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();

