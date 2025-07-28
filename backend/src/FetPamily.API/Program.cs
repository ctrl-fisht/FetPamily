using FetPamily.Application;
using FetPamily.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI(c =>
    {
        
    });
}

app.MapControllers();

app.Run();
