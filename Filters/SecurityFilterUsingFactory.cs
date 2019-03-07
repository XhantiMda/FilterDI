using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using FilterDI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace FilterDI.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SecurityFilterUsingFactory : Attribute, IAuthorizationFilter, IFilterFactory
    {
        private IAuthenticationService _authenticationService;
        private ILogger _logger;

        public SecurityFilterUsingFactory(IAuthenticationService authenticationService, ILogger<string> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // gets the dependecies from the serviceProvider 
            // and creates an instance of the filter
            return new SecurityFilterUsingFactory(
                (IAuthenticationService)serviceProvider.GetService(typeof(IAuthenticationService)),
                (ILogger<string>)serviceProvider.GetService(typeof(ILogger<string>)));
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
                _logger.LogError(exception, "Failed to authticate user");
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}