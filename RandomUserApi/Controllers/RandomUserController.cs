
using RandomUserApi.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RandomUserApi.Services;
using System.Text.Json;


namespace DevaloreAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomUserController : ControllerBase
    {
        private readonly ILogger<RandomUserController> _logger;
        private readonly UserService _userService;

        public RandomUserController(ILogger<RandomUserController> logger)
        {
            _logger = logger;

        }

        [HttpGet("by-gender/{gender:sex}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ModelStateDictionary))]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersData([FromRoute] string gender)
        {
            var users = await _userService.GetUsersData(gender);

            if (users is null)
                ModelState.AddModelError(nameof(gender), "No users with that gender");

            _logger.LogInformation("Users with the gender selected");
            return new JsonResult(users, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        [HttpGet("popular-country")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            var country = await _userService.GetMostPopularCountry();
            return country == null ? NotFound() : Ok(country);
        }

        [HttpGet("list-of-mails/{format?}")]
        [FormatFilter]
        [Produces("application/json")]
        public async Task<IActionResult> GetListOfMails()
        {
            var mails = await _userService.GetListOfMails();
            return Ok(mails);
        }

        [HttpGet("oldest-user")]
        public async Task<IActionResult> GetTheOldestUser()
        {
            var users = await _userService.GetOldestUser();
            return Ok(users);
        }

    }
}
