using credit.customers.Clients;
using credit.customers.Controllers;
using credit.customers.Data;
using credit.customers.Exceptions;
using credit.customers.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DelinquenciesService>();
builder.Services.AddScoped<LitigationsService>();

builder.Services.AddHttpClient<LoanClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Services:LoanService"]
        ?? throw new InvalidOperationException(
            "Services:LoanService is missing"));
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    db.Database.Migrate();
}
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();