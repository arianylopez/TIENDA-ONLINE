using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TiendaOnline.Inventario;
using TiendaOnline.GestionUsuario;

namespace TiendaOnline.Ventas
{
    class Carrito
    {
        private ProductoCarrito[] items;
        private int totalItems;

        public Carrito(int capacidad)
        {
            items = new ProductoCarrito[capacidad];
            totalItems = 0;
        }

        public bool AgregarItem(Producto producto, int cantidad)
        {
            for(int i = 0; i < totalItems; i++)
            {
                if (items[i].Producto.Codigo == producto.Codigo)
                {
                    items[i].Cantidad += cantidad;
                    return true;
                }
            }

            if(totalItems < items.Length)
            {
                items[totalItems] = new ProductoCarrito(producto, cantidad);
                totalItems++;
                return true;
            }
            return false;
        }

        public bool EliminarItem(int codigoProducto)
        {
            for(int i = 0; i < totalItems; i++)
            {
                if (items[i].Producto.Codigo == codigoProducto)
                {
                    for(int j = i; j < totalItems - 1; j++)
                    {
                        items[j] = items[j + 1];
                    }
                    totalItems--;
                    return true;
                }
            }
            Console.WriteLine("Producto no encontrado en el carrito.");
            return false;
        }

        public float CalcularTotal()
        {
            float total = 0;
            for(int i = 0; i < totalItems; i++)
            {
                total += items[i].SubTotal;
            }
            return total;
        }

        public void MostrarCarrito()
        {
            if(totalItems == 0)
            {
                Console.WriteLine("El carrito esta vacio.");
                return;
            }
            Console.WriteLine("--- Carrito de Compras ---");
            for (int i = 0; i < totalItems; i++)
            {
                Console.WriteLine($"Producto: {items[i].Producto.Nombre}");
                Console.WriteLine($"Cantidad: {items[i].Cantidad}");
                Console.WriteLine($"Precio Unitario: ${items[i].Producto.Precio}");
                Console.WriteLine($"Subtotal: ${items[i].SubTotal}");
            }
            Console.WriteLine($"Total: ${CalcularTotal()}");
        }

        public ProductoCarrito[] ObtenerItems()
        {
            ProductoCarrito[] productosCarrito = new ProductoCarrito[totalItems];
            for(int i = 0; i < totalItems; i++)
            {
                productosCarrito[i] = items[i];
            }
            return productosCarrito;
        }

        public int ObtenerCantidadItems()
        {
            return totalItems;
        }
    }
}
