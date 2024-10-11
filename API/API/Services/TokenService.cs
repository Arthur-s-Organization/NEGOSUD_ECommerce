using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Models;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;

	public TokenService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GenerateJwtToken(Customer user)
	{
		// Créer une liste de claims qui seront inclus dans le token (par exemple, l'email ou l'ID de l'utilisateur)
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Email, user.Email)
		};

		// Générer la clé de sécurité pour signer le token
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		// Créer le token
		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Issuer"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),  // Token valide pendant 30 minutes
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}