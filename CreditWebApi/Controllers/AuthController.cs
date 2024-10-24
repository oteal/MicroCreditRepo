using CreditWebApi.Models;
using CreditWebApi.Persitence;
using CreditWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditWebApi.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase {
		private readonly IChaveMovelDigital _cmdService;
		public AuthController(IChaveMovelDigital cmdService) {
			_cmdService = cmdService;
		}

		[HttpPost, Route("Login", Name = "Login")]
		public IActionResult Login([FromBody] CMDRequest request) {
			var token = _cmdService.Login(request.PhoneNumber, request.Password);
			if(!token.HasValue || token == Guid.Empty) {
				return Unauthorized("Invalid Chave Móvel Digital credentials.");
			}

			var user = Storage.Users.FirstOrDefault(m => m.Value.Phonenumber == request.PhoneNumber).Value;
			if(user != null) {
				user.CMDToken = token.Value;
			}

			return Ok(new { Token = token });
		}

		[HttpPost, Route("Logout", Name = "Logout")]
		public IActionResult Logout([FromBody] Guid token) {
			_cmdService.Logout(token);
			return Ok("Logout successful!");
		}

	}
}