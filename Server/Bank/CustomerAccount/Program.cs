using CustomerAccount.Api.Midllewares;
using CustomerAccount.Service;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Services.Extensions;
using Microsoft.Data.SqlClient;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddServiceExtension(builder.Configuration.GetConnectionString("Zipi"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHandlerErrorsMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
