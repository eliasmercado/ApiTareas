using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTareas.DTOs;
using WebTareas.Models;
using WebTareas.Services;
using WebTareas.Utils;

namespace WebTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService TokenService;

        public LoginController(IConfiguration configuration)
        {
            TokenService = new TokenService(configuration);
        }


        [HttpPost]
        public ApiResponse<object> Authenticate(LoginDTO login)
        {
            try
            {
                ApiResponse<object> response = new ApiResponse<object>();

                //Vamos a validar las credenciales
                if (!login.UserName.Equals("edge"))
                    throw new ApiException("El usuario no existe.");

                if (!login.Password.Equals("123456"))
                    throw new ApiException("Credenciales incorrectas.");

                //Obtenemos el token que va utilizar para las peticiones autenticadas
                response.Data = TokenService.CreateToken(login.UserName);
                
                return response;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
