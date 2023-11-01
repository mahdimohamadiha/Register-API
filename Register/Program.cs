using Register.Services.Registrations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<IRegistrationsService, RegistrationsService>();
}

var app = builder.Build();
{
    // app.UseExceptionHandler("/error");
    app.UseHttpsRedirection(); 
    app.MapControllers();
    app.Run();
}

