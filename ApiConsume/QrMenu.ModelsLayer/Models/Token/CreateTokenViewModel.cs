using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HotelAndTours.ModelsLayer.Models.Token;

public class CreateTokenViewModel
{
    public string TokenCreate()
    {
        var bytes = Encoding.UTF8.GetBytes("hotelandtoursapiproject");
        var key = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken("https://localhost", "https://localhost", notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(3), signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public string TokenCreateForAdmin()
    {
        var bytes = Encoding.UTF8.GetBytes("hotelandtoursapiproject");
        var key = new SymmetricSecurityKey(bytes);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, "Admin"),
            new(ClaimTypes.Role, "Visitor")
        };
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken("https://localhost",
            "https://localhost",
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(3),
            signingCredentials: credentials, claims: claims);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }
}