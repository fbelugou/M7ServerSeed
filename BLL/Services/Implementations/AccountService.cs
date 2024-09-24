using BLL.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BLL.Services.Implementations;
internal class AccountService : IAccountService
{

    private readonly IConfiguration _configuration;

    public AccountService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string LogIn(string username, string password)
    {
    
        if(username == "admin" && password == "admin")
        {
            return GenerateToken(new User()
            {
                Id = 1,
                Name = "admin",
                FirstName = "admin",
                Email = "admin@admin.com",
                Username = "admin",
                Roles = new List<string>() {"ADMIN", "USER" }
            });
        }
        else if (username == "user" && password == "user")
        {
            return GenerateToken(new User()
            {
                Id = 2,
                Name = "user",
                FirstName = "user",
                Email = "user@user.com",
                Username = "user",
                Roles = new List<string>() {"USER"}

            });

        }
        
        throw new LogInException(username);
    }


    
    //Private 
    //Générer un token jwt 
    //entrée : utilisateur
    //retour : token jwt
    private string GenerateToken(User user)
    {
        // Add User Infos
        var claims = new Dictionary<string, object>() {
            [JwtRegisteredClaimNames.Sub] = user.Id.ToString(),
            [ClaimTypes.NameIdentifier] = user.Id.ToString(),
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
        
            //Add Roles
            [ClaimTypes.Role] = user.Roles
         };

        //Signing Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Expiration time
        var expires = DateTime.Now.AddSeconds(_configuration.GetValue<int>("JWTExpireTokenInSeconds"));

        //Create JWT Token Object
        var token = new SecurityTokenDescriptor()
        {
            Issuer = _configuration.GetValue<string>("JWTIssuer"),
            Audience = _configuration.GetValue<string>("JWTAudience"),
            Claims = claims,
            Expires = expires,
            SigningCredentials= creds
        };

        //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
        return new JsonWebTokenHandler().CreateToken(token);
    }
}
