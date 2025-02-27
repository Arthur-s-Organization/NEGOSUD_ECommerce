﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Http;

public class TokenService : ITokenService
{
	private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
	{
		_configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
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

	public string AssignToken(string token)
	{
        // Ajoute le token dans un cookie HttpOnly
        var options = new CookieOptions
        {
            HttpOnly = true, // Empêche l'accès via JS
            Secure = false, // Utilise HTTPS
            SameSite = SameSiteMode.Strict, // Empêche l'accès cross-site
            Expires = DateTime.UtcNow.AddDays(1)
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("negosudToken", token, options);
        return token;

    }

	public void RemoveCurentToken()
	{
        // Supprime le cookie en utilisant le même nom et les mêmes options
        var options = new CookieOptions
        {
            HttpOnly = true, // Assure la sécurité
            Secure = false, // Utilise HTTPS
            SameSite = SameSiteMode.Strict, // Empêche l'accès cross-site
            Expires = DateTime.UtcNow.AddDays(-1) // Expire immédiatement pour forcer la suppression
        };

        // Supprime le cookie en le réécrivant avec une date d'expiration passée
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("negosudToken");
		return;
    }
}