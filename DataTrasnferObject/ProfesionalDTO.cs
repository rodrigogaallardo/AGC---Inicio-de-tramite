using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ProfesionalDTO
    {
        public int Id { get; set; }
        public int? IdConsejo { get; set; }
        public int? IdGrupoConsejo { get; set; }
        public string Matricula { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? NroDocumento { get; set; }
        public string Calle { get; set; }
        public string NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string UnidadFuncional { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Sms { get; set; }
        public string Telefono { get; set; }
        public string Cuit { get; set; }
        public long? IngresosBrutos { get; set; }
        public string Inhibido { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CreateUser { get; set; }
        public string CreateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool BajaLogica { get; set; }
        public int? MatriculaMetrogas { get; set; }
        public int? CategoriaMetrogas { get; set; }
        public string Titulo { get; set; }
        public bool InhibidoBit { get; set; }

        //public int id_rel_emp_usu { get; set; }

        public string ApeNom { 
            get 
            {
                return Apellido + " " + Nombre;
            } 
        }
        public string Direccion {
            get
            {
                return Calle + " " + (NroPuerta == null || NroPuerta.Equals("0") ? "" : NroPuerta);
            }
        }
        
        public string DadoBaja { get
            {
                return (BajaLogica) ? "Si" : "No";
            }
        }
        public string observaciones { get; set; }
        public virtual ConsejoProfesionalDTO ConsejoProfesionalDTO { get; set; }

        public string Bloqueado
        {
            get
            {
                if (UserAspNet!=null)
                    return (UserAspNet.IsLockedOut) ? "Si" : "No";
                return "";
            }
        }
        public virtual AspnetUserDTO UserAspNet { get; set; }
        public string UsuarioCreado
        {
            get
            {
                return (UserAspNet != null) ? UserAspNet.UserName : "";
            }
        }
    }
}
