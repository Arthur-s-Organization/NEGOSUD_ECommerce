using API.Models;

public interface ITokenService
{
	string GenerateJwtToken(Customer user);
    public string AssignToken(string token);
}