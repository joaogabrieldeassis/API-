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

//M�todo para configurar o meu appsettings.json
void LoadConfiguration(WebApplication app)
{
    
}

//M�todo da Configura��o da minha Autentica��o e autoriza��o
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    
}

//M�todo para Desabilitar a valida��o automatica da minha api
void ConfigureMVC(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

//M�todo para servi�os Do banco e Token
void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
    builder.Services.AddScoped<DataContext, DataContext>();
    builder.Services.AddMvc();
}