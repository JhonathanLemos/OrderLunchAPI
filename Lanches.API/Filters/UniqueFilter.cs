using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Lanches.API.Filters
{
    public class UniqueFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DbUpdateException dbUpdateException)
            {

                var sqlException = dbUpdateException.GetBaseException() as Microsoft.Data.SqlClient.SqlException;
                if (sqlException != null && sqlException.Number == 2601)
                {
                    context.Result = new BadRequestObjectResult("O nome fornecido já está em uso. Por favor, escolha um nome diferente.");
                    context.ExceptionHandled = true;
                }
            }

            base.OnException(context);
        }
    }
}
