using HotelManagement.Application;
using HotelManagement.Application.Mapping;
using HotelManagement.Infrastructure;
using HotelManagement.Infrastructure.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services
builder.Services.AddApplication();    // Extension method
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMappings();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();

