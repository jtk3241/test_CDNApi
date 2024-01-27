using CompleteDevNet.Core.Interfaces;
using CompleteDevNet.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace CompleteDevNet.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : Controller
{
    private readonly JwtSettings _jwtSettings;
    private readonly Serilog.ILogger _logger;
    private readonly IAccountService _accountService;

    public AccountController(
        JwtSettings jwtSettings,
        Serilog.ILogger logger,
        IAccountService accountService
    )
    {
        _jwtSettings = jwtSettings;
        _logger = logger;
        _accountService = accountService;
    }

    /// <summary>
    /// Request for JWT using user sign-in details.
    /// </summary>
    /// <param name="userLogin">user code and password</param>
    /// <returns>JWT string and other relevant information.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserTokenModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<UserTokenModel>, BadRequest, UnauthorizedHttpResult>> GetToken(UserLoginModel userLogin)
    {
        string sUserName = userLogin.UserName;
        if (string.IsNullOrEmpty(sUserName))
        {
            return TypedResults.BadRequest();
        }

        //*** TODO: implement authentication function
        bool bPassAuthentication = false;
        bPassAuthentication = await _accountService.AuthenticateUser(userLogin.UserName, userLogin.Password);

        if (bPassAuthentication)
        {
            UserTokenModel objReturn;
            var userToken = new UserTokenModel
            {
                GuidId = Guid.NewGuid(),
                UserName = sUserName,
                ExpiredTime = DateTime.UtcNow.AddMonths(1),
            };
            var token = JwtHelpers.JwtHelpers.GenTokenkey(userToken, _jwtSettings);
            objReturn = token;
            _logger.Information($"GetToken authenticate user successful, UserName:{sUserName}");
            return TypedResults.Ok(objReturn);
        }
        else
        {
            _logger.Warning($"GetToken authenticate user failed, UserName:{sUserName}");
            return TypedResults.Unauthorized();
        }

    }
}
