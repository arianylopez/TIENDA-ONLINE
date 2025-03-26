using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.GestionUsuario
{
    class GestorUsuarios
    {
        private Usuario[] usuarios;
        private int totalUsuarios;

        public GestorUsuarios(int capacidad)
        {
            usuarios = new Usuario[capacidad];
            totalUsuarios = 0;
        }

        private Usuario BuscarUsuario(int id)
        {
            for(int i = 0; i < totalUsuarios; i++)
            {
                if (usuarios[i].Id == id)
                {
                    return usuarios[i]; 
                }
            }
            return null;
        }
        public bool RegistrarCliente(int id, string nombre, string correo, string contraseña, string direccion = "", string telefono = "")
        {
            if(BuscarUsuario(id) != null)
            {
                Console.WriteLine("Usuario ya registrado");
                return false;
            }
            if(totalUsuarios < usuarios.Length)
            {
                usuarios[totalUsuarios] = new Cliente(id, nombre, correo, contraseña, direccion, telefono);
                totalUsuarios++;
                return true;
            }
            return false;
        }

        public bool RegistrarAdministrador(int id, string nombre, string correo, string contraseña, string cargo = "Administrador")
        {
            if(BuscarUsuario(id) != null)
            {
                Console.WriteLine("Ya existe un usuario con este ID.");
                return false;
            }

            if(totalUsuarios < usuarios.Length)
            {
                usuarios[totalUsuarios] = new Administrador(id, nombre, correo, contraseña, cargo);
                totalUsuarios++;
                return true;
            }
            return false;
        }

        public Usuario IniciarSesionCliente(int id, string contraseña)
        {
            Usuario usuario = BuscarUsuario(id);
            if (usuario != null && usuario is Cliente && usuario.Contraseña == contraseña)
            {
                return usuario;
            }
            Console.WriteLine("Datos Incorrectos!");
            return null;
        }

        public Usuario IniciarSesionAdmin(int id, string contraseña)
        {
            Usuario usuario = BuscarUsuario(id);
            if (usuario != null && usuario is Administrador && usuario.Contraseña == contraseña)
            {
                return usuario;
            }
            Console.WriteLine("Datos Incorrectos!");
            return null;
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("--- Lista de Usuarios ---");
            for(int i = 0; i < totalUsuarios; i++)
            {
                Console.WriteLine($"Id: {usuarios[i].Id}");
                Console.WriteLine($"Nombre: {usuarios[i].Nombre}");
                Console.WriteLine($"Cargo: {(usuarios[i] is Administrador ? "Administrador" : "Cliente")}");
            }
        }
    }
}
