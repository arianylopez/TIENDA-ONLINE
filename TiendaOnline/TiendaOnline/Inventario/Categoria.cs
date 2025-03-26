using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Inventario
{
    public class Categoria
    {
        public int Id;
        public string Nombre;

        public Categoria(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"ID Categoria: {Id}");
            Console.WriteLine($"Nombre Categoria: {Nombre}");
        }
    }
}
