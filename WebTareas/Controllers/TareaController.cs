using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTareas.Models;
using WebTareas.Service;
using WebTareas.Utils;

namespace WebTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private readonly TareaService TareaService;

        public TareaController(WebTareasDbContext context)
        {
            TareaService = new TareaService(context);
        }


        [HttpGet]
        public ApiResponse<List<Tarea>> Get()
        {
            try
            {
                ApiResponse<List<Tarea>> response = new ApiResponse<List<Tarea>>
                {
                    Data = TareaService.GetTareas()
                };

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

        [HttpGet("{id}")]
        public ApiResponse<Tarea> Get(int id)
        {
            try
            {
                ApiResponse<Tarea> response = new ApiResponse<Tarea>
                {
                    Data = TareaService.GetTareaById(id)
                };

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

        [HttpPost]
        public ActionResult<ApiResponse<Tarea>> Post(Tarea tarea)
        {
            try
            {
                ApiResponse<Tarea> response = new ApiResponse<Tarea>
                {
                    Data = TareaService.AddTarea(tarea)
                };

                return Created("", response);
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

        [HttpPut("{id}")]
        public ApiResponse<object> Put(int id, Tarea tarea)
        {
            try
            {
                ApiResponse<object> response = new ApiResponse<object>
                {
                    Data = TareaService.UpdateTarea(id, tarea)
                };

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

        [HttpDelete("{id}")]
        public ApiResponse<object> Delete(int id)
        {
            try
            {
                ApiResponse<object> response = new ApiResponse<object>
                {
                    Data = TareaService.DeleteTarea(id)
                };

                return response;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
