using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TestPoint.Application;
using TestPoint.Cache;
using TestPoint.DAL;
using TestPoint.EmailService;
using TestPoint.JwtService;
using TestPoint.WebAPI.Database;
using TestPoint.WebAPI.HttpClients;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

var builder = WebApplication.CreateBuilder(args);


#region Services

builder.Services.AddLogging(b => b.AddLog4Net("log4net.config", true));
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddDal(builder.Configuration);
builder.Services.AddEmailService(builder.Configuration);
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddHttpClients(builder.Configuration);

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<FormOptions>(opt =>
{
    opt.KeyLengthLimit = int.MaxValue;
    opt.ValueCountLimit = int.MaxValue;
    opt.ValueLengthLimit = int.MaxValue;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtAuthSettings:TokenSecurityKey").Value!)),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration.GetSection("JwtAuthSettings:Issuer").Value!,
            ValidAudience = builder.Configuration.GetSection("JwtAuthSettings:Audience").Value!
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

#endregion


var app = builder.Build();
app.MigrateDatabase(); // Code-first DB creation


#region HTTP request pipeline

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
