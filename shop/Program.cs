using Microsoft.EntityFrameworkCore;
using Shop.Data;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shop;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigureAuthentication(builder);
ConfigureMVC(builder);
ConfigureServices(builder);

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
LoadConfiguration(app);
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c=>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1");
}
);

app.UseRouting();
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

//Método para configurar o meu appsettings.json
void LoadConfiguration(WebApplication app)
{
    
}

//Método da Configuração da minha Autenticação e autorização
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Settings.Secrete);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
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
    builder.Services.AddCors();
    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
    });
    builder.Services.AddControllers();
    //builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Shop Api", Version = "v1" });
    });
    builder.Services.AddMvc();
}