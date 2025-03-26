using System;
using TiendaOnline.Inventario;
using TiendaOnline.GestionUsuario;

namespace TiendaOnline.Ventas
{
    public class ProductoCarrito
    {
        public Producto Producto;
        public int Cantidad;

        public float SubTotal => Producto.Precio * Cantidad;

        public ProductoCarrito(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;
        }
    }
}
