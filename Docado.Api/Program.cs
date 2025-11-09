using Docado.BusinessLogic.ServiceCollectionExtensions;
using Docado.DataAccess.DataAccessEntensions;
using Microsoft.AspNetCore.Authentication.Cookies;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new Exception("Connection string not found");

builder.Services.AddDocadoDataAccess(connectionString);
builder.Services.AddDocadoServices();
builder.Services.AddAuthorization();
builder.Services.AddDocadoIdentity();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.MaxAge = TimeSpan.FromDays(30);
    });

WebApplication app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.UseHttpsRedirection();

app.Run();