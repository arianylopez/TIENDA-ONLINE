using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaOnline.Pagos
{
    public enum TipoMetodoPago
    {
        Efectivo,
        TarjetaCredito,
        TarjetaDebito,
        QR
    }
    class MetodoPago
    {
        public TipoMetodoPago Tipo;
        public string Descripcion;
        public MetodoPago(TipoMetodoPago tipo)
        {
            Tipo = tipo;
            switch (tipo)
            {
                case TipoMetodoPago.Efectivo:
                    Descripcion = "Pago en efectivo";
                    break;
                case TipoMetodoPago.TarjetaCredito:
                    Descripcion = "Pago con Tarjeta de Crédito";
                    break;
                case TipoMetodoPago.TarjetaDebito:
                    Descripcion = "Pago con Tarjeta de Débito";
                    break;
                case TipoMetodoPago.QR:
                    Descripcion = "Pago en QR";
                    break;
                default:
                    Descripcion = "Método de pago no especificado";
                    break;
            }
        }
    }

}
