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

    /// <summary>
    /// Get list of registered developers.
    /// </summary>
    /// <param name="pagingParameters">Provide paging parameters. Page number starts from 0. If none are provided, defaults will be used (pagesize = 200, pagenumber = 0) </param>
    /// <returns></returns>
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

    /// <summary>
    /// Register new developer.
    /// </summary>
    /// <remarks>
    /// Email used for registration a new developer should be unique.
    /// </remarks>
    /// <param name="developer"></param>
    /// <returns>Registered developer with identity-ID</returns>
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

    /// <summary>
    /// Update developer details.
    /// </summary>
    /// <remarks>Note: developers email can be changed, but it must be unique across all developers.</remarks>
    /// <param name="IdentGuid">Developer's identity Guid.</param>
    /// <param name="developer">Developer details.</param>
    /// <returns>Updated developer details.</returns>
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

    /// <summary>
    /// Remove developer from registry.
    /// </summary>
    /// <param name="IdentGuid">Developer's identity Guid.</param>
    /// <returns>Http 200 (Ok).</returns>
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
