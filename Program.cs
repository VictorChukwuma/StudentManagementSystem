using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

                                                                                                                                                                                                                            
// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddSingleton<AuthService>();



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);
                        options.TokenValidationParameters = new TokenValidationParameters
                                {
                                            ValidateIssuer = true,
                                                        ValidateAudience = true,
                                                                    ValidateIssuerSigningKey = true,
                                                                                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                                                                                            ValidAudience = builder.Configuration["JwtSettings:Audience"],
                                                                                                        IssuerSigningKey = new SymmetricSecurityKey(key)
                                                                                                                };
                                                                                                                    });

                                                                                                                    builder.Services.AddAuthorization();

var app = builder.Build();
app.MapControllers();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

 
 app.UseAuthentication();
 app.UseAuthorization();

// Minimal GET endpoint
app.MapGet("/", () => "Server is running");

// Start the web serverf
app.Run();
