using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebTareas.Utils;

namespace WebTareas
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ApiException ae)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                ApiError error = new ApiError()
                {
                    Error = new ErrorMessage()
                    {
                        Message = ae.Message
                    }
                };

                var errorJson = JsonConvert.SerializeObject(error);

                await response.WriteAsync(errorJson);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ApiError error = new ApiError()
                {
                    Error = new ErrorMessage()
                    {
                        Message = "Error interno del servidor. Mensaje: " + ex.Message
                    }
                };

                var errorJson = JsonConvert.SerializeObject(error);

                await response.WriteAsync(errorJson);
            }
        }
    }

    public class ApiError
    {
        [JsonProperty("error")]
        public ErrorMessage Error { get; set; }
    }

    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
