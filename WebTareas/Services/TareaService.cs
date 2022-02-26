using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTareas.Models;
using WebTareas.Utils;

namespace WebTareas.Service
{
    public class TareaService
    {
        private readonly WebTareasDbContext _context;

        public TareaService(WebTareasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todas las tareas registradas en la BD.
        /// </summary>
        /// <returns></returns>
        public List<Tarea> GetTareas()
        {
            return _context.Tareas.ToList();
        }

        /// <summary>
        /// Obtiene la tarea correspondiente al id enviado, si no encuentra propaga una excepcion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tarea GetTareaById(int id)
        {
            Tarea tarea = _context.Tareas.Find(id);

            if(tarea == null)
            {
                throw new ApiException("La tarea no existe.");
            }

            return tarea;
        }

        /// <summary>
        /// Inserta una nueva tarea en la base de datos.
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        public Tarea AddTarea(Tarea tarea)
        {
            if (TareaExists(tarea.Id))
            {
                throw new ApiException("Ya existe una tarea con el mismo id.");
            }

            _context.Tareas.Add(tarea);
            _context.SaveChanges();

            return tarea;
        }

        /// <summary>
        /// Actualiza una tarea, si no encuentra la tarea propaga una excepcion.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tarea"></param>
        /// <returns></returns>
        public string UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                throw new ApiException("El id enviado no coincide con el id del objeto.");
            }

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    throw new ApiException("La tarea no existe.");
                }
                else
                {
                    throw;
                }
            }

            return "Tarea modificada correctamente";
        }

        /// <summary>
        /// Elimina una tarea, si no encuentra propaga una excepcion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteTarea(int id)
        {
            var tarea = _context.Tareas.Find(id);
            if (tarea == null)
            {
                throw new ApiException("La tarea no existe.");
            }

            _context.Tareas.Remove(tarea);
            _context.SaveChanges();

            return "Tarea eliminada correctamente";
        }

        /// <summary>
        /// Retorna true si la tarea existe, false en caso contrario.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }

    }
}
