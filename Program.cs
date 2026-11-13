using CenterPsy_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<UsuarioService>(cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:3000/");
});

builder.Services.AddHttpClient<ContatoService>(cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:3000/");
});

builder.Services.AddScoped<LoginService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Angular");

app.UseAuthorization();

app.MapControllers();

app.Run();
