using System.Text.Json;
using CrudWebApi.Data;
using CrudWebApi.Repositories;
using CrudWebApi.Services;
using Microsoft.EntityFrameworkCore;


var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();

                      });
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//ajout de mon service IuserSevice Userservice
// j'utilise la méthode AddScoped une instance par requete
builder.Services.AddScoped<IUserService, UserService>();

//ajout de mon service IUserRepository UserRepository
// j'utilise la méthode AddScoped
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();



// // Renvoie du camelCase en JSON au front
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
//ce qui configure l'app avant le build / ce qui démarre,utilise l'app apreès le build
var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();


