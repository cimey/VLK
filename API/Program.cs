using API.ExceptionHandler;
using API.Validators;
using Application.DependencyInjection;
using FluentValidation;
using Infrastructure.DatabaseContext;
using Infrastructure.DependencyInjection;
using Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterApplication();
builder.Services.RegisterInfra(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<BuyStockRequestValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
    {
        b.AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VLKContext>();
    db.Database.Migrate();
    await DbInitializer.SeedAsync(db);
}
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
