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
            return NotFound();
            //return Json("nulo");
        }

        [Route("Usuario/ObtenerUsuario/{id_usuario?}")]
        public async Task<IActionResult> Obtener([FromRoute] string id_usuario)
        {
            
            var allPersons = await nusuario.ListaUsuarios();// GetAllPersons();
           
            var lista = allPersons.Where(x => x.id_usuario == id_usuario).FirstOrDefault();
      
            return Json(new {data =lista });
           
        }


        [Route("Usuario/ObtenerTaxista/{id_usuario?}")]
        public async Task<IActionResult> ObtenerTaxista([FromRoute] string id_usuario)
        {
            // var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            var allPersons = await nusuario.ListaTaxista();// GetAllPersons();
            //  await firebase
            //    .Child("Usuarios")
            //   .OnceAsync<Usuario>();
            var lista = allPersons.Where(x => x.id_usuario == id_usuario).FirstOrDefault();

            return Json(new { data = lista });
            // return allPersons.Where(a => a.id_usuario == id_usuario).FirstOrDefault();
        }

    }
}
