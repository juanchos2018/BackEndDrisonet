using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace BackEndDrisonet.Controllers
{
    public class SolicitudesController : Controller
    {
        NSolicitudes solicitud = new NSolicitudes();
        public IActionResult Index()
        {
            return View();
        }

        [Route("Solicitude/GetSolicitudes")]
        public async Task<IActionResult> Get_Solicitudes([FromBody] Publicacion o)
        {
            string key_usu = o.key_noticia;
            var listasolicitudes = await solicitud.Lista_Solicitudes(key_usu);
           // var lista  = listasolicitudes.Where(x=>x.id)
            return Json(listasolicitudes);
        }
    }
}
