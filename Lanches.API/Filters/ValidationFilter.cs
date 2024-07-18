using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lanches.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            if (context.Exception != null)
            {

                context.Result = new ObjectResult(new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Ocorreu um erro durante a execução do método {actionName} do controller {controllerName}. Detalhes: {context.Exception.Message}"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
                _logger.LogError(context.Exception, $"Exceção não tratada no método {actionName} do controller {controllerName}.");

            }

            _logger.LogInformation($"Finalizando execução do método {actionName} no controller {controllerName}.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;
            var parameters = string.Join(", ", context.ActionArguments.Select(kvp => $"{kvp.Key}: {kvp.Value}"));

            if (!context.ModelState.IsValid)
            {
                var messages = context.ModelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(messages);
                _logger.LogWarning($"Validação falhou no método {actionName} no controller {controllerName}. Parâmetros: {parameters}. Erros: {string.Join(", ", messages)}");
            }

            _logger.LogInformation($"Iniciando execução do método {actionName} no controller {controllerName} com parâmetros {parameters}");
        }
    }
}
