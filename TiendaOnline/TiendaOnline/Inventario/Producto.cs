using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Inventario
{
    public class Producto
    {
        public int Codigo;
        public string Nombre;
        public float Precio;
        public int Stock;
        public Categoria Categoria;

        public Producto(int codigo, string nombre, float precio, int stock, Categoria categoria)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"Codigo Producto: {Codigo}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Precio: {Precio}");
            Console.WriteLine($"Stock: {Stock}");
            Console.WriteLine("Categoria: ");
            Categoria.MostrarDetalles();
        }
    }
}
