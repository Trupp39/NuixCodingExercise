using InvestmentPerformanceAPI.DTOs;
using InvestmentPerformanceAPI.Models;
using InvestmentPerformanceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentPerformanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserInvestmentService _userInvestmentService;

        public UserController(ILogger<UserController> logger, IUserInvestmentService userInvestmentService)
        {
            _logger = logger;
            _userInvestmentService = userInvestmentService;
        }

        //get endpoint exposing a given users investments
        [HttpGet("{userId}/investments")]
        public async Task<IActionResult> GetUserInvestments(int userId)
        {
            _logger.LogInformation("GET: /" + userId + "/investments");
            var investments = await _userInvestmentService.GetUserInvestments(userId);

            if (investments == null)
            {
                _logger.LogInformation($"User with ID {userId} not found.");
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(investments);
        }

        //get endpoint exposing a users certain investment details
        [HttpGet("{userId}/investment/{investmentId}")]
        public async Task<IActionResult> GetUserInvestments(int userId, int investmentId)
        {
            _logger.LogInformation("GET: /" + userId + "/investment/" + investmentId);
            var investment = await _userInvestmentService.GetUserInvestmentDetails(userId, investmentId);

            if (investment == null)
            {
                _logger.LogInformation($"Investment with ID {investmentId} not found.");
                return NotFound($"Investment with ID {investmentId} not found.");
            }

            return Ok(investment);
        }
    }
}
