using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndDrisonet.Controllers
{
    public class ChatController : Controller
    {
        NSubChat su = new NSubChat();
        public IActionResult Index()
        {
            return View();
        }
        [Route("Chat/Mensaje")]
        public IActionResult CrearUsuario([FromBody] SubChat o)
        {
            bool completado = false;
            Task tarea = Task.Run(() => su.SetMensaje(o));
            if (tarea.IsCompleted)
            {
                completado = true;
            }
            return Json(completado);
        }
    }
}
