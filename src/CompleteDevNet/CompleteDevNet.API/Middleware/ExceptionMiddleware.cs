using Newtonsoft.Json;
using System.Net;
using ILogger = Serilog.ILogger;

namespace CompleteDevNet.API.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public ExceptionMiddleware(
        ILogger logger
    )
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            string errorId = Guid.NewGuid().ToString();
            string sSource = exception.TargetSite?.DeclaringType?.FullName ?? "";
            string sException = exception.Message.Trim();
            string sQueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value ?? "" : "";
            string sRequestPath = context.Request.Path.ToUriComponent();

            _logger.Debug($"Error at Error Id: [{errorId}], Source:[{sSource}], RequestPath:[{sRequestPath}], QueryString:[{sQueryString}].");
            _logger.Error($"Error Id: [{errorId}], " + exception.ToString());

            var errorResult = new ErrorResult
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                Exception = sException,
                ErrorId = errorId,
                SupportMessage = $"Provide this message to the support team for further analysis. Error Id: [{errorId}], Source:[{sSource}], RequestPath:[{sRequestPath}], QueryString:[{sQueryString}]."
            };
            errorResult.Messages.Add(exception.Message);
            if (/*exception is not CustomException &&*/ exception.InnerException != null)
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }

            switch (exception)
            {
                default:
                    errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var response = context.Response;
            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
            }
            else
            {
                throw;
            }
        }
    }


}
