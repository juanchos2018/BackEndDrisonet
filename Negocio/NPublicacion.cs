using Entidad;
using Firebase.Auth;
using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                string link = await task; 
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
                var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");
                var key_producto = Firebase.Database.FirebaseKeyGenerator.Next();

                await firebase
                  .Child("Noticias")
                  .PostAsync(new Publicacion() { nombre_usuario = o.nombre_usuario, img_usuario = o.img_usuario, descripcion_noticia = o.descripcion_noticia, img_noticia = linkimage });


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }
    }
}
