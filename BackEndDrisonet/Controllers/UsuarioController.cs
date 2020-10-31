using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace BackEndDrisonet.Controllers
{
    public class UsuarioController : Controller
    {
        NUsuario nusuario = new NUsuario();
        public IActionResult Index()
        {
            return View();
        }
        [Route("Usuario/SetUsuario")]
        public IActionResult CrearUsuario([FromBody]Usuario o)
        {
            bool completado = false;            
            Task tarea = Task.Run(() => nusuario.CrearUsuario(o));
            if (tarea.IsCompleted)
            {
                completado = true;
            }
            return Json(completado);
        }
    }
}
