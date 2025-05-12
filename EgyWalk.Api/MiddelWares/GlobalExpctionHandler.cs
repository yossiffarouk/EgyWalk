using System.Net;

namespace EgyWalk.Api.MiddelWares
{
    public class GlobalExpctionHandler
    {
        private readonly ILogger logger;
        private readonly RequestDelegate requestDelegate;
   

        public GlobalExpctionHandler( ILogger<GlobalExpctionHandler> logger , RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
           
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                var ErorrId = Guid.NewGuid().ToString();
                logger.LogError(ex, $"{ErorrId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var erorr = new
                {
                    id = ErorrId,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };


                await httpContext.Response.WriteAsJsonAsync(erorr);
            }
        }
    }
}
