using CrudWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//ajout de mon service IuserSevice Userservice
// j'utilise la méthode AddScoped une instance par requete
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddControllers();
//ce qui configure l'app avant le build / ce qui démarre,utilise l'app apreès le build
var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.Run();


