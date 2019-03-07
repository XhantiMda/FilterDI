using System;
using System.Reflection;
using FilterDI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class SecurityFilter : IAuthorizationFilter
{
    private IAuthenticationService _authenticationService;
    private ILogger _logger;

    public SecurityFilter(IAuthenticationService authenticationService, ILogger<string> logger)
    {
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];

        try
        {
            if (!_authenticationService.Autheticate(token))
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                return;
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to authorize user");
            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
        }
    }

}