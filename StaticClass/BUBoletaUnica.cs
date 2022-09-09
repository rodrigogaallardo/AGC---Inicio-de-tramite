using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClass
{
    public class BUBoletaUnica
    {
        public int IdBoletaUnica { get; set; }
        public int IdPago { get; set; }
        public string CodBarras { get; set; }
        public long NroBoletaUnica { get; set; }
        public int Dependencia { get; set; }
        public BUDatosContribuyente Contribuyente { get; set; }
        public decimal MontoTotal { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaAnulada { get; set; }
        public DateTime? FechaCancelada { get; set; }
        public string TrazaPago { get; set; }
        public string CodigoVerificador { get; set; }
        public string NroBUI { get; set; }
        public Guid? BUI_ID { get; set; }

        public DateTime? BuscarFechaPago(string traza)
        {
            DateTime? fechaPago = null;

            if (string.IsNullOrEmpty(traza))
                return fechaPago;

            traza = traza.Trim().ToLower();

            if (traza.Equals("ingreso no autorizado"))
                return fechaPago;

            if (traza.Contains("inexistente"))
                return fechaPago;

            if (traza.Length < 20)
                return fechaPago;

            //Formato de la traza


            //"00222013061312142400446831002163160611";
            //POS	            4 digitos	Numero de caja	
            //FECHA DE PAGO	4 digitos	Fecha de pago	        aaaaMMddhhmmss
            //LEGAJO	        8 digitos	Legajo del cajero	
            //OT	            8 digitos	Numero interno	
            //PP	            4 digitos	Numero de impresor	

            int anio = Convert.ToInt16(traza.Substring(4, 4));
            int mes = Convert.ToInt16(traza.Substring(8, 2));
            int dia = Convert.ToInt16(traza.Substring(10, 2));
            int hora = Convert.ToInt16(traza.Substring(12, 2));
            int minuto = Convert.ToInt16(traza.Substring(14, 2));
            int segundo = Convert.ToInt16(traza.Substring(16, 2));

            try
            {
                //System.Globalization.CultureInfo cultura = new System.Globalization.CultureInfo("en-US"); //, cultura.Calendar);
                fechaPago = new DateTime(anio, mes, dia, hora, minuto, segundo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return fechaPago;
        }

    }

}
