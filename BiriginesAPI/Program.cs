using System.Data.SqlClient;
using System.Data;
using Tools.CQRS;
using System.IdentityModel.Tokens.Jwt;
using ToolBox.JWT.Services;
using ToolBox.JWT.Configuration;
using Tools.JWT.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("default");
// Add services to the container.


builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddHandlersAndDispatcher();
JWTConfiguration jwtConfiguration = builder.Configuration.GetSection("Jwt").Get<JWTConfiguration>();
builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<IToken,JWTService> ();
builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
 ).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters{
     ValidateAudience = jwtConfiguration.Audience is not null ,
     ValidAudience = jwtConfiguration.Audience,
     ValidateIssuer= jwtConfiguration.Issuer is not null ,
     ValidIssuer= jwtConfiguration.Issuer,
     ValidateLifetime = jwtConfiguration.Duration is not null , 
     ValidateIssuerSigningKey = true,
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Signature)),
 });



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
