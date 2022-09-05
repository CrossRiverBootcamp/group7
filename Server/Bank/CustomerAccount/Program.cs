using CoronaApp.Api.Midllewares;
using CustomerAccount.Service;
using CustomerAccount.Service.Interfaces;
using CustomerAccount.Services.Extensions;
using CustomerAccount.Storage;
using CustomerAccount.Storage.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddServiceExtension(builder.Configuration.GetConnectionString("zipi"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseHandlerErrorsMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
