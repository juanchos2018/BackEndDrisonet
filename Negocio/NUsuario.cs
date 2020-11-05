﻿using Entidad;
using Firebase.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
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
                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo, o.password, o.nombre_usuario, true);
                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                client = new FireSharp.FirebaseClient(conexion.conec());
                var data = o;
                data.key_usuario = id;
                SetResponse setResponse = client.Set("Usuarios/" + data.key_usuario, data);               

            }
            catch (Exception ex)
            {

            }
            return true;
        }

    }
}
