using CompleteDevNet.API.Models;
using CompleteDevNet.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace CompleteDevNet.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class UserController : Controller
{
    private readonly Serilog.ILogger _logger;
    private readonly IUserService _userService;

    public UserController(
        Serilog.ILogger logger,
        IUserService userService
    )
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("/UserList")]
    [ProducesResponseType(typeof(List<UserModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<Results<Ok<List<UserModel>>, NotFound>> GetUserList()
    {
        return TypedResults.Ok(new List<UserModel>() { new UserModel { Name = "xxx" } });
    }



}
