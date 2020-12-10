using Entidad;
using Firebase.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class NUsuario
    {
        private Conexion conexion;
        private IFirebaseClient client;

        public async Task<bool> CrearUsuario(Usuario o)
        {
            try
            {
                conexion = new Conexion();
                var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo_usuario, o.password, o.nombre_usuario, true);
                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                client = new FireSharp.FirebaseClient(conexion.conec());
                var data = o;
                data.id_usuario = id;
                SetResponse setResponse = client.Set("Usuario/" + data.id_usuario, data);               

            }
            catch (Exception ex)
            {

            }
            return true;
        }
      

        public async Task<List<Usuario>> ListaUsuarios()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Usuario")
              .OnceAsync<Usuario>()).Select(item => new Usuario
              {
                  nombre_usuario = item.Object.nombre_usuario,
                  apellido_usuario = item.Object.apellido_usuario,
                  dni_usuario=item.Object.dni_usuario,
                  image_usuario=item.Object.image_usuario,
                  correo_usuario=item.Object.correo_usuario,
              
                  id_usuario=item.Object.id_usuario
              }).ToList();
        }

        public async Task<List<Usuario>> ListaTaxista()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://fir-app-cf755.firebaseio.com/");

            return (await firebase
              .Child("Usuarios")
              .OnceAsync<Usuario>()).Select(item => new Usuario
              {
                  nombre_usuario = item.Object.nombre_usuario,
                  apellido_usuario = item.Object.apellido_usuario,
                  dni_usuario = item.Object.dni_usuario,
                  image_usuario = item.Object.image_usuario,
                  correo_usuario = item.Object.correo_usuario,
                  token = item.Object.token,
                  id_usuario = item.Object.id_usuario
              }).ToList();
        }

    }
}
