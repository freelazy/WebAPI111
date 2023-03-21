using System.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "nf173",
        ValidAudience = "everyone",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("12345123451234512345123451234523"))
    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy(name: "xxx",
                policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5173");
                });
    o.AddPolicy(name: "yyy",
                policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5173")
                    .AllowCredentials();
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors("yyy");

app.UseAuthorization();

app.MapControllers();

app.Run();
