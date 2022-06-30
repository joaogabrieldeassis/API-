using Microsoft.EntityFrameworkCore;
using Shop.Data;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMVC(builder);
ConfigureServices(builder);

var app = builder.Build();
LoadConfiguration(app);


app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();

//Método para configurar o meu appsettings.json
void LoadConfiguration(WebApplication app)
{
    
}

//Método da Configuração da minha Autenticação e autorização
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    
}

//Método para Desabilitar a validação automatica da minha api
void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

//Método para serviços Do banco e Token
void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
    builder.Services.AddScoped<DataContext, DataContext>();
    builder.Services.AddMvc();
}