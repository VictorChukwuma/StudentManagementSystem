using StudentManagementSystem.Models;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace StudentManagementSystem.Services
{
    public class AuthService
        {
                private readonly IConfiguration _config;

                        public AuthService(IConfiguration config)
                                {
                                            _config = config;
                                                    }

                                                            public string GenerateJwtToken(Student student)
                                                                    {
                                                                                var jwtSettings = _config.GetSection("JwtSettings");
                                                                                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
                                                                                                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                                                                                                                    var claims = new[]
                                                                                                                                {
                                                                                                                                                new Claim(JwtRegisteredClaimNames.Sub, student.Id.ToString()),
                                                                                                                                                                new Claim(JwtRegisteredClaimNames.Email, student.Email)
                                                                                                                                                                            };

                                                                                                                                                                                        var token = new JwtSecurityToken(
                                                                                                                                                                                                        issuer: jwtSettings["Issuer"],
                                                                                                                                                                                                                        audience: jwtSettings["Audience"],
                                                                                                                                                                                                                                        claims: claims,
                                                                                                                                                                                                                                                        expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                                                                                                                                                                                                                                                                        signingCredentials: creds
                                                                                                                                                                                                                                                                                    );

                                                                                                                                                                                                                                                                                                return new JwtSecurityTokenHandler().WriteToken(token);
                                                                                                                                                                                                                                                                                                        }
                                                                                                                                                                                                                                                                                                            }
                                                                                                                                                                                                                                                                                                            }