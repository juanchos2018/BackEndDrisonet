using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Negocio;
using Newtonsoft.Json;
namespace BackEndDrisonet.Controllers
{
    public class PublicarController : Controller
    {

        private readonly IHostingEnvironment _env;

        NPublicacion publicar = new NPublicacion();
        public PublicarController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("Publicar/SubirPublicacion")]
        [HttpPost("[action]")]
        public IActionResult SubirArchivos(IFormCollection UploadedFiles)
        {
            string filePath = "";
            Publicacion publica = new Publicacion();
            publica.nombre_usuario = UploadedFiles["nombre_usuario"].ToString();
            publica.descripcion_noticia = UploadedFiles["descripcion_noticia"].ToString();
            publica.img_usuario = UploadedFiles["image_empresa"].ToString();
            publica.key_usuario = UploadedFiles["key_usuario"].ToString();
            publica.titulo = UploadedFiles["titulo"].ToString();
            publica.telefono = UploadedFiles["telefono"].ToString();
            long size = UploadedFiles.Files.Sum(f => f.Length);
            string ruta = Path.Combine(_env.ContentRootPath, "/Content/Images/");          
            string fileName = "";
            
                foreach (var formFile in UploadedFiles.Files)
                {
                    if (formFile.Length > 0)
                    {
                        fileName = formFile.FileName;
                        filePath = Path.Combine(ruta, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            formFile.CopyTo(fileStream);
                        }
                    }
                }
           
            if (size==0)
            {                
                Task task = Task.Run(() => publicar.Upload(null, fileName, publica));
                return Ok(new { archivos = UploadedFiles.Files.Count, size, ruta });
            }
            else
            {
                System.IO.FileStream stream1;
                stream1 = new FileStream(Path.Combine(filePath), FileMode.Open);
                Task task = Task.Run(() => publicar.Upload(stream1, fileName, publica));
                return Ok(new { archivos = UploadedFiles.Files.Count, size, ruta });
            }          
           
        }

        [Route("Publicar/GetPublicaciones")]
        public async Task<IActionResult> Get_Publicaciones([FromBody] Usuario o)
        {   
            var ListaPublicaciones = await publicar.Lista_Publicacion();
            var lista = ListaPublicaciones.Where(x => x.key_usuario == o.id_usuario).ToList();
            return Json(lista);
        }

        //[Route("Publicar/Set_Notificar")]
        //public  IActionResult Set_Notificar(string  token)
        //{
        //    token="c2Oe5XpHRJ6isxwmhARfgB:APA91bE3BBgJcupCGvCrKNd7FLwht316_SlJ1t_kEff_ss00a3UkHluHc33M15UXkdE-Ow9z5WRd2IRGgYNcyoPalN43EH1LP78b048XUgA6pGgZIOIv7WM9mXFz9jInbbD0FLPJKBP2";
        //    EnviarNotificacion(token);
        //    return Ok();
        //}
       
    }
}
