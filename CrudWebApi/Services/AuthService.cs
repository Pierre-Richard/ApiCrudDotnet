using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using CrudWebApi.Data;
using CrudWebApi.Models;
using Microsoft.IdentityModel.Tokens;

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

        private string GenerateToken(User user)
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

        public string Register(User user)
        {

            //Recevoir firstName, lastName, email, password
            //Hacher le password avec BCrypt
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 13);
            user.Password = hashPassword;
            //ajouter le mot de passe
            _appDbContext.Add(user);
            // sauvegarder le user
            _appDbContext.SaveChanges();

            return GenerateToken(user);
          
        }

        public string Login(User user)
        {
            //je regarde dans la db si utilisateur existe via son email
            var userdb = _appDbContext.Users.FirstOrDefault((u) => u.Email == user.Email);
            if (userdb != null)
            {   //verifier le mdp en base avec celui envoyé par l'utilisation (body)
                var verifyPassword = BCrypt.Net.BCrypt.Verify(user.Password,userdb.Password);
                //si le password existe 
                if (verifyPassword)
                {
                    return GenerateToken(user);

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
