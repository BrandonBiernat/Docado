using Docado.DataAccess.DB;
using Docado.DataAccess.Implementations;
using Docado.DataAccess.Implementations.Users;
using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Identity Types
builder.Services.AddIdentity<UserRecord, IdentityRole>(options => { })
    .AddDefaultTokenProviders();

// Identity Services
builder.Services.AddTransient<IUserStore<UserRecord>, UserStore>();
string connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new Exception("Connection string not found");
builder.Services.AddDbContext<DocadoDbContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityApiEndpoints<UserRecord>()
    .AddEntityFrameworkStores<DocadoDbContext>();

builder.Services.Configure<IdentityOptions>(options => {
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    
    options.SignIn.RequireConfirmedEmail = true;

    options.Password.RequiredLength = 12;
    
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.Configure<PasswordHasherOptions>(option => {
    option.IterationCount = 12000;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = "Docado.Auth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Identity/Account/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

builder.Services.AddControllers();

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

app.MapIdentityApi<UserRecord>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();