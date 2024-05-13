using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyCollection.Application.Common.Helpers;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Common.Settings;
using MyCollection.Infrastructure.Common.Helpers;
using MyCollection.Infrastructure.Services;
using MyCollection.Persistence.Brokers;
using MyCollection.Persistence.Brokers.Interfaces;
using MyCollection.Persistence.DataContext;
using MyCollection.Persistence.Repositories;
using MyCollection.Persistence.Repositories.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.AddDbContext<CollectionDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IPasswordHasher, PasswordHasher>()
    .AddScoped<ITokenGeneratorService, TokenGeneratorService>()
    .AddScoped<IAccountService, AccountService>();

builder.Services.AddSingleton<ICacheBroker, LazyCacheBroker>();

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;

        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = jwtSettings.ValidateIssuer,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            ValidateAudience = jwtSettings.ValidateAudience,
            ValidateLifetime = jwtSettings.ValidateLifetime,
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            }
        };

    });

builder.Services.AddLazyCache();
builder.Services.AddRouting(o => o.LowercaseUrls = true);
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
