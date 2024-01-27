using CompleteDevNet.API.Models;
using CompleteDevNet.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace CompleteDevNet.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class DeveloperController : Controller
{
    private readonly Serilog.ILogger _logger;
    private readonly IDeveloperService _developerService;

    public DeveloperController(
        Serilog.ILogger logger,
        IDeveloperService developerService
    )
    {
        _logger = logger;
        _developerService = developerService;
    }

    [HttpGet("/UserList")]
    [ProducesResponseType(typeof(List<DeveloperModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<Results<Ok<List<DeveloperModel>>, NotFound>> GetDeveloperList()
    {
        var developerList = await _developerService.GetDeveloperList();
        if (developerList != null && developerList.Count > 0)
        {
            var result = developerList.Select(DeveloperModel.From).ToList();
            return TypedResults.Ok(result);
        }
        else
        {
            return TypedResults.NotFound();
        }
    }
}
