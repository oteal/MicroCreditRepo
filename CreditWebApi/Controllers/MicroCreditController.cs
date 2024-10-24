using CreditWebApi.Models;
using CreditWebApi.Persitence;
using CreditWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreditWebApi.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class MicroCreditController : ControllerBase {
    private readonly IChaveMovelDigital _cmdService;
    private readonly CreditService _creditService;
    public MicroCreditController(IChaveMovelDigital cmdService) {
      _cmdService = cmdService;
      _creditService = new CreditService();
    }

    [HttpPost, Route("Subscribe", Name = "Subscribe")]
    public IActionResult Subscribe([FromQuery] decimal amount, [FromBody] Guid token) {
      /*
      if(!Guid.TryParse(token, out Guid tokenGuid)) {
        return BadRequest("Invalid Authorization token format.");
      }
      */
      if(!_cmdService.IsValidToken(token)) {
        return Unauthorized("Invalid token");
			}

      var user = UserService.GetUser(token);
      if(user == null) {
        return Unauthorized("User not found");
			}

      if(!_creditService.IsValidAmount(user, amount, out decimal maxAmount)) {
        return Unauthorized($"Credit amount is not allowed. The maximum is {maxAmount}");
      }

      if(_creditService.CalculateRisk(user, amount) >= RiskLevel.MEDIUM) {
        return Unauthorized("Calculated risk is high so we cannot aprove the credit.");
      }

      _creditService.Subscribe(user, amount);


      return Ok("Credit subscribed");
    }
  }
}