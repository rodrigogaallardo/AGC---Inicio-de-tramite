using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class Profesional_historialDTO
    {
        public int Id { get; set; }
        public string tipoOperacion { get; set; }
        public string OperacionFull
        {
            get
            {
                switch (this.tipoOperacion)
                {
                    case "I":
                        return "Alta";
                    case "D":
                        return "Baja";
                    default:
                        return "Modificación";
                }
            }
        }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.Guid> UsuarioResponsable { get; set; }
        public int Id_profesional { get; set; }
        public Nullable<int> IdConsejo { get; set; }
        public string Matricula { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> IdTipoDocumento { get; set; }
        public string TipoDocuento
        {
            get
            {
                return TipoDocumentoPersonal.Nombre;
            }
        }
        public Nullable<int> NroDocumento { get; set; }
        public string Calle { get; set; }
        public string NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Sms { get; set; }
        public string Telefono { get; set; }
        public string Cuit { get; set; }
        public Nullable<long> IngresosBrutos { get; set; }
        public string Inhibido
        {
            get
            {
                return ((bool)InhibidoBit) ? "Si" : "No";
            }
        }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public bool BajaLogica { get; set; }
        public string Baja { get { return (this.BajaLogica) ? "Si" : "No"; } }
        public Nullable<int> MatriculaMetrogas { get; set; }
        public Nullable<int> CategoriaMetrogas { get; set; }
        public string Titulo { get; set; }
        public Nullable<bool> InhibidoBit { get; set; }
        public string observaciones { get; set; }
        public virtual AspnetUserDTO aspnet_Users { get; set; }
        public virtual ConsejoProfesionalDTO ConsejoProfesional { get; set; }
        public virtual TipoDocumentoPersonalDTO TipoDocumentoPersonal { get; set; }
        public AspnetUserDTO aspnet_Users1 { get; set; }

        public string Direccion
        {
            get
            {
                return string.Format("{0} {1} {2} {3},{4} {5}", this.Calle, this.NroPuerta, this.Piso, this.Depto, this.Localidad, this.Provincia);
            }
        }

        public string Perfiles { get; set; }
        //public string Perfiles
        //{
        //    get
        //    {
        //        string aux = "";
        //        if (aspnet_Users != null)
        //        {
        //            foreach (var item in aspnet_Users.AspNetRoles)
        //            {
        //                aux += item.Description + " - ";
        //            }
        //            aux = aux.Remove(aux.Length - 2);
        //        }
        //        return aux;
        //    }
        //}
    }
}
