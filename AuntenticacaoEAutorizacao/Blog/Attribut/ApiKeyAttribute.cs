using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attribut;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Query.TryGetValue(Configuration.Api_key, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey Não encontrada"
            };
        }
        if (!Configuration.ApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = "Acesso não autorizado"
            };

        }
        await next();
    }
}

