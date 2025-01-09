using Bookshop.Application.DependencyInjection;
using Bookshop.Infra.Data.DependencyInjection;
using Bookshop.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddPostgreSqlConnection(builder.Configuration)
    .AddRepositories(builder.Configuration)
    .AddBusiness(builder.Configuration);


var app = builder.Build(); 

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting(); 
app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers(); 
});

app.MapFallbackToFile("/index.html");

app.Run();
