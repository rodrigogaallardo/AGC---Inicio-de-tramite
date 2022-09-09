using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClass
{
    public class BUDatosContribuyente
    {
        public Constantes.BUTipoPersona TipoPersona { get; set; }
        public string ApellidoyNombre { get; set; }
        public Nullable<Constantes.BUTipodocumento> TipoDoc { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string CodPost { get; set; }
        public string Email { get; set; }
        public string TipoDocumentoValue { get; set; }
        public string TipoPersonaValue { get; set; }

        public bool ValidarDatosContribuyente()
        {
            //TipoPersona, , TipoDoc,  , Piso, Departamento, , , Email
            if (!validardatosobligatorios())
                return false;

            return true;

        }
        private bool validardatosobligatorios()
        {

            if (string.IsNullOrEmpty(this.ApellidoyNombre))
            {
                throw new BUDatosContribuyenteException("Debe indicar apellido y nombre del contribuyente.");
            }
            if (this.TipoDoc != null)
            {
                if (this.Documento.Length == 0)
                {
                    throw new BUDatosContribuyenteException("El número de documento debe ser un valor positivo.");
                }
            }
            else
            {
                throw new BUDatosContribuyenteException("Debe indicar el tipo de documento.");
            }

            if (string.IsNullOrEmpty(this.Direccion))
            {
                throw new BUDatosContribuyenteException("Debe indicar la dirección del contribuyente.");
            }

            if (string.IsNullOrEmpty(this.Localidad))
            {
                throw new BUDatosContribuyenteException("Debe indicar la localidad del contribuyente.");
            }

            //if (string.IsNullOrEmpty(this.CodPost))
            //{
            //    throw new BUDatosContribuyenteException("Debe indicar el código postal del contribuyente.");
            //}

            if (!string.IsNullOrEmpty(this.Email))
            {
                if (!Funciones.validarEmail(this.Email))
                {
                    throw new BUDatosContribuyenteException("Email invalido.");
                }
            }

            return true;
        }
    }
    public class BUDatosContribuyenteException : Exception
    {
        public BUDatosContribuyenteException(string mensaje)
            : base(mensaje, new Exception())
        {
        }
    }
}
