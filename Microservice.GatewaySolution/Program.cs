using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();


var settingsConfig = builder.Configuration.GetSection("ApiSettings");

var issuer = settingsConfig["Issuer"];
var audience = settingsConfig["Audience"];
var secret = settingsConfig["Secret"];

var key = Encoding.ASCII.GetBytes(secret);


builder.Services.AddAuthentication(c =>
{
    c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(c =>
{
    c.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience
    };
});

builder.Services.AddAuthorization();



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot();


app.UseAuthentication();
app.UseAuthorization();

app.Run();
