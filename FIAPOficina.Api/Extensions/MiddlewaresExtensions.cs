using FIAPOficina.Api.Middlewares;

namespace FIAPOficina.Api.Extensions
{
    public static class MiddlewaresExtensions
    {
        public static void AddMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}