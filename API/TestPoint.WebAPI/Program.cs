using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TestPoint.Application;
using TestPoint.Cache;
using TestPoint.DAL;
using TestPoint.DAL.Contexts;
using TestPoint.Domain;
using TestPoint.EmailService;
using TestPoint.JwtService;
using TestPoint.WebAPI.Middlewares.CustomExceptionHandler;

var builder = WebApplication.CreateBuilder(args);


#region Services

builder.Services.AddLogging(b => b.AddLog4Net("log4net.config", true));
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddDal(builder.Configuration);
builder.Services.AddJwtService();
builder.Services.AddEmailService();
builder.Services.AddApplication();

#endregion

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Jwt:TokenSecurityKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
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



var app = builder.Build();

#region HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

InitializeDatabase(); // Code-first DB creation

app.Run();

#endregion


void InitializeDatabase()
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();
        context.Database.Migrate();

        var defaultAdmin = new Administrator // [makima, makima12345]
        {
            Login = new SystemLogin
            {
                LoginType = LoginType.Administrator,
                Username = "makima",
                PasswordHash = "i7phShT1JsaP7dLz05tc1FzmSiixX5pIuexRMQlrerdq2qtiVIMEQNnHfJ8U+CUlRHouGETIuUL+BYMD4hGOsw==OxoaYEtzmx3EyLg3Oex6qzZzMeKgPcUJLNQViqKphZs=",
                PasswordReseted = false,
                RegistryDate = DateTime.MinValue
            }
        };

        var adminsSet = context.Set<Administrator>();
        if (adminsSet.Include(x => x.Login).FirstOrDefault(x => x.Login.Username == defaultAdmin.Login.Username && x.Login.LoginType == LoginType.Administrator) is null)
        {
            context.Set<Administrator>().Add(defaultAdmin);
            context.SaveChanges();
        }
    }
}