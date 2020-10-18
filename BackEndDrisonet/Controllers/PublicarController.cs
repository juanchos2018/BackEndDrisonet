using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;

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
            Publicacion noticia = new Publicacion();

            noticia.nombre_usuario = UploadedFiles["nombre_usuario"].ToString();
            noticia.descripcion_noticia = UploadedFiles["descripcion_noticia"].ToString();
            noticia.img_usuario = UploadedFiles["image_empresa"].ToString();
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
            System.IO.FileStream stream1;
            stream1 = new FileStream(Path.Combine(filePath), FileMode.Open);
            Task task = Task.Run(() => publicar.Upload(stream1, fileName, noticia));
            return Ok(new { archivos = UploadedFiles.Files.Count, size, ruta });
        }

    }
}
