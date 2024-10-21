using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<Customer> _userManager;
		private readonly SignInManager<Customer> _signInManager;
		private readonly ITokenService _tokenService;

		public AuthController(UserManager<Customer> userManager, SignInManager<Customer> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(CustomerRequestDTO model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Vérifier si l'email existe déjà
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				return BadRequest("User with this email already exists.");
			}

			// Créer l'objet Customer avec les champs supplémentaires
			var customer = new Customer
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Gender = model.Gender,
				DateOfBirth = model.DateOfBirth,
				PhoneNumber = model.PhoneNumber,
				AddressId = model.AddressId
			};

			// Créer l'utilisateur avec le mot de passe
			var result = await _userManager.CreateAsync(customer, model.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			else //if (result.Succeeded)
			{
				var token = _tokenService.GenerateJwtToken(customer);
				return Ok(new { Token = token });
			}

		}

		[HttpPut("update/{id}")]
		public async Task<IActionResult> UpdateCustomer(string id,[FromBody] UpdateCustomerRequestDTO model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var customer = await _userManager.FindByIdAsync(id.ToString());
			if (customer == null)
			{
				return Unauthorized("Customer not found.");
			}

			if (!string.IsNullOrEmpty(model.Email) && model.Email != customer.Email)
			{
				var setEmailResult = await _userManager.SetEmailAsync(customer, model.Email);
				if (!setEmailResult.Succeeded)
				{
					return BadRequest(setEmailResult.Errors);
				}
			}

			// Mise à jour mot de passe
			if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword))
			{
				var passwordCheckResult = await _userManager.CheckPasswordAsync(customer, model.OldPassword);
				if (!passwordCheckResult)
				{
					return BadRequest("The old password is incorrect.");
				}

				var passwordChangeResult = await _userManager.ChangePasswordAsync(customer, model.OldPassword, model.NewPassword);
				if (!passwordChangeResult.Succeeded)
				{
					return BadRequest(passwordChangeResult.Errors);
				}
			}

			customer.FirstName = model.FirstName;
			customer.LastName = model.LastName;
			customer.PhoneNumber = model.PhoneNumber;
			customer.Gender = model.Gender;
			customer.DateOfBirth = model.DateOfBirth;
			customer.AddressId = model.AddressId;
		
			var updateResult = await _userManager.UpdateAsync(customer);
			if (!updateResult.Succeeded)
			{
				return BadRequest(updateResult.Errors);
			}

			return Ok("Customer updated successfully!");
		}



		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			var customer = await _userManager.FindByEmailAsync(model.Username);

			if (customer == null)
			{
				return Unauthorized("Invalid login attempt.");
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);


			if (!result.Succeeded)
				return Unauthorized("Invalid login attempt.");

			else//if (result.Succeeded) 
			{
				 var token = _tokenService.AssignToken(_tokenService.GenerateJwtToken(customer));
				return Ok(new { Token = token , UserId = customer.Id });
			}
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			_tokenService.RemoveCurentToken();
			return Ok("Logged out successfully!");
		}
	}
}
