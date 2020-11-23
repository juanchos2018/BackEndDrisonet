using Microsoft.AspNetCore.Mvc;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndDrisonet.Controllers
{
    public class PapeletaController : Controller
    {
        NPapeleta npapeleta = new NPapeleta();
        public IActionResult Index()
        {
            return View();
        }
        [Route("Papeleta/ObtenerPapeleta/{id_usuario?}")]
        public async Task<IActionResult> ObtenerPapeletas([FromRoute] string id_usuario)
        {
          //  string key_usu = o.key_noticia;
            var listasoPapeletas = await npapeleta.Lista_Papeletas(id_usuario);
            return Json(listasoPapeletas);

        }
    }
}
