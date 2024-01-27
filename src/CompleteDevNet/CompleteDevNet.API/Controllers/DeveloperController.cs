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

    [AllowAnonymous]
    [HttpGet("/Developers")]
    [ProducesResponseType(typeof(List<DeveloperModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<Results<Ok<List<DeveloperModel>>, NotFound>> GetDeveloperList(
        [FromQuery] PagingParameters? pagingParameters = null
        )
    {
        var developerList = pagingParameters != null ?
            await _developerService.GetDeveloperList(
                pagingParameters.PageSize.GetValueOrDefault(pagingParameters.DefaultPageSize),
                pagingParameters.PageNumber.GetValueOrDefault(pagingParameters.DefaultPageNumber)
                )
            : await _developerService.GetDeveloperList();
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

    [HttpPost("/Developers")]
    [ProducesResponseType(typeof(DeveloperModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<DeveloperModel>, BadRequest<ValidationProblemDetails>, UnprocessableEntity<string>>> RegisterDeveloper(DeveloperNewModel developer)
    {
        var objInput = DeveloperNewModel.ToCore(developer);
        //*** simple validations
        try
        {
            var objResult = await _developerService.RegisterDeveloper(objInput);
            if (objResult != null)
            {
                var objReturn = DeveloperModel.From(objResult);
                return TypedResults.Ok(objReturn);
            }
            else
            {
                return TypedResults.UnprocessableEntity("No records processed.");
            }
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Processing Error", $"{ex.Message}");
            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            problemDetails.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return TypedResults.BadRequest(problemDetails);
        }
    }

    [HttpPut("/Developers/{IdentGuid}")]
    [ProducesResponseType(typeof(DeveloperModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok<DeveloperModel>, BadRequest<ValidationProblemDetails>, UnprocessableEntity<string>>> UpdateDeveloper(Guid IdentGuid, DeveloperNewModel developer)
    {
        var objInput = DeveloperNewModel.ToCore(developer);
        objInput.IdentGuid = IdentGuid;
        //*** simple validations
        try
        {
            var objResult = await _developerService.UpdateDeveloper(objInput);
            if (objResult != null)
            {
                var objReturn = DeveloperModel.From(objResult);
                return TypedResults.Ok(objReturn);
            }
            else
            {
                return TypedResults.UnprocessableEntity("No records processed.");
            }
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Processing Error", $"{ex.Message}");
            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            problemDetails.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return TypedResults.BadRequest(problemDetails);
        }
    }

    [HttpDelete("/Developers/{IdentGuid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<Results<Ok, BadRequest<ValidationProblemDetails>, UnprocessableEntity<string>>> DeleteDeveloper(Guid IdentGuid)
    {
        try
        {
            await _developerService.DeleteDeveloper(IdentGuid);
            return TypedResults.Ok();
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("Processing Error", $"{ex.Message}");
            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            problemDetails.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return TypedResults.BadRequest(problemDetails);
        }
    }
}
