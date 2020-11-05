using Entidad;
using Firebase.Auth;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio
{
   public class NPublicacion
    {

        private Conexion conexion;
      

        public async Task<bool> Upload(FileStream stream, string filenanme, Publicacion obj)
        {
            conexion = new Conexion();
            string link = "";
            if (stream!=null)
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
                var a = await auth.SignInWithEmailAndPasswordAsync(conexion.AthEmail(), conexion.AthPassword());

                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    conexion.Storge(),
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                    .Child("Publicaciones")
                    .Child(filenanme)
                    .PutAsync(stream, cancellation.Token);
                     link = await task;
              }
          
            try
            {
                
                Task tarea2 = Task.Run(() => Create_Publicacion(obj, link));
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            return true;
        }
        public async Task<bool> Create_Publicacion(Publicacion o, string linkimage)
        {
            try
            {
                string ruta = "";
                if (linkimage=="")
                {
                    ruta = "default_image";
                }
                else
                {
                    ruta = linkimage;
                }
                var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
                var key_Noticia = Firebase.Database.FirebaseKeyGenerator.Next();

                await firebase
                  .Child("Publicaciones").Child(key_Noticia)
                  .PutAsync(new Publicacion() {key_noticia=key_Noticia,key_usuario=o.key_usuario, nombre_usuario = o.nombre_usuario, img_usuario = o.img_usuario, descripcion_noticia = o.descripcion_noticia, img_noticia = ruta,fecha_noticia=DateTime.Now.Date.ToShortDateString() });

            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }

        public async Task<List<Publicacion>> Lista_Publicacion()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
            return (await firebase
              .Child("Publicaciones")
              .OnceAsync<Publicacion>()).Select(item => new Publicacion
              {
                  key_noticia=item.Object.key_noticia,
                  titulo=item.Object.titulo,
                  descripcion_noticia = item.Object.descripcion_noticia,
                  img_noticia=item.Object.img_noticia,
                  fecha_noticia=item.Object.fecha_noticia,
                  key_usuario=item.Object.key_usuario,
                  contador=item.Object.contador
                  
              }).ToList();
        }

    }
}
