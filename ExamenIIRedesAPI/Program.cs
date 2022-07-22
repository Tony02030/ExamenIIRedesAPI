using Microsoft.OpenApi.Models;
using System.Reflection;
using ExamenIIRedesAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("GetAllPolicy",
      builder =>
      {
          builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();//PUT, PATCH, GET, DELETE
      });
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "VacunaDOS API",
        Description = "Academic project used to learn RESTful APIs",
        
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();
    options.SchemaFilter<SwaggerSchemaExampleFilter>();
});

var app = builder.Build();
app.UseSwagger(c =>
{
    c.RouteTemplate = "api-docs/swagger/{documentname}/swagger.json";

});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api-docs/swagger/v1/swagger.json", "VacunaDOS");
    c.RoutePrefix = "api-docs";


});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors("GetAllPolicy");

app.MapControllers();

app.Run();