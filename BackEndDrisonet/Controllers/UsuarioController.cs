using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Newtonsoft.Json;

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
        public IActionResult CrearUsuario([FromBody] Usuario o)
        {
            bool completado = false;
            Task tarea = Task.Run(() => nusuario.CrearUsuario(o));
            if (tarea.IsCompleted)
            {
                completado = true;
            }
            return Json(completado);
        }

        [Route("Usuario/ConsultaRuc")]
        public async Task<ActionResult> Get_Consulta_Ruc([FromBody] Empresa o)
        {
            HttpClient client = new HttpClient();
            String URL2 = "https://quertium.com/api/v1/sunat/ruc/" + o.ruc + "?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.MTM3Mw.x-jUgUBcJukD5qZgqvBGbQVMxJFUAIDroZEm4Y9uTyg";
            HttpResponseMessage response = await client.GetAsync(URL2);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var empresa = JsonConvert.DeserializeObject<Empresa>(data);
                return Json(empresa);
            }

            return Json("sin Datos");

        } 
        public void Consulata()
        {
            Console.WriteLine("cmsulta");

        }
    }
}
