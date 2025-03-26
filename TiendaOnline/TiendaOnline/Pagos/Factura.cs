using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaOnline.Ventas;
using TiendaOnline.GestionUsuario;

namespace TiendaOnline.Pagos
{
    class Factura
    {
        public int NumeroFactura;
        public Cliente Cliente;
        public Venta Venta;
        public MetodoPago MetodoPago;
        public DateTime FechaEmision;

        public Factura(int numeroFactura, Cliente cliente, Venta venta, MetodoPago metodoPago, DateTime fechaEmision)
        {
            NumeroFactura = numeroFactura;
            Cliente = cliente;
            Venta = venta;
            MetodoPago = metodoPago;
            FechaEmision = fechaEmision;
        }

        public void MostrarDetalles()
        {
            Console.WriteLine($"Número de Factura: {NumeroFactura}");
            Console.WriteLine($"Fecha de Emisión: {FechaEmision}");
            Console.WriteLine($"Cliente: {Cliente.Nombre}");
            Console.WriteLine($"Método de Pago: {MetodoPago.Descripcion}");
            Console.WriteLine("Detalles de Venta:");
            Venta.MostrarDetalles();
        }
    }
}
