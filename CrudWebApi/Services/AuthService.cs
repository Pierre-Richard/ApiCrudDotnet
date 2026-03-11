using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using CrudWebApi.Data;
using CrudWebApi.DTOs;
using CrudWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace CrudWebApi.Services
{
    public class AuthService : IAuthService
    {
        //J'inject AppDbContext dans mon construteur 

        private AppDbContext _appDbContext;
        private IConfiguration _configuration;
        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        private string GenerateToken(AuthResponseDto user)
        {
              //Générer un token JWT
            // Les infos qu'on met dans le token
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
            );
            //Retourner le token  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthResponseDto Register(RegisterDto user)
        {
              //Hacher le password avec BCrypt
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 13);
            user.Password = hashPassword;
            // créer un objet User à partir de registerDTO
            var newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,

            };       
          
            //ajouter le mot de passe
            _appDbContext.Add(newUser);
            // sauvegarder le user
            _appDbContext.SaveChanges();

            //Crée authResponseDto avec les infos du user sans le token
            var authResponseDto = new AuthResponseDto()
            {   Id = newUser.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            //Génère le token en passant authResponseDto
            var generateToken = GenerateToken(authResponseDto);
            // Assigne le token à authResponseDto.Token
            authResponseDto.Token = generateToken;
          
            return authResponseDto;
          
        }

        public AuthResponseDto Login(LoginDto user)
        {
            //je regarde dans la db si utilisateur existe via son email
            var userdb = _appDbContext.Users.FirstOrDefault((u) => u.Email == user.Email);
            if (userdb != null)
            {   //verifier le mdp en base avec celui envoyé par l'utilisation (body)
                var verifyPassword = BCrypt.Net.BCrypt.Verify(user.Password,userdb.Password);
                //si le password existe 
                if (verifyPassword)
                {
                    //Crée authResponseDto avec les infos du user sans le token
                    var authResponseDto = new AuthResponseDto()
                    {
                        Id = userdb.Id,
                        FirstName = userdb.FirstName,
                        LastName = userdb.LastName,
                        Email = userdb.Email,
                    };

                    //Génère le token en passant authResponseDto
                    var generateToken = GenerateToken(authResponseDto);

                    authResponseDto.Token = generateToken;

                    return authResponseDto;


                }
                else
                {
                    //sinon le password n'exite pas
                    throw new Exception("Le password n'existe pas");
                }

            } else
            {
                //sinon le user n'exite pas
                throw new Exception("L'Utilisateur n'existe pas");
            }


        }
    }


}
