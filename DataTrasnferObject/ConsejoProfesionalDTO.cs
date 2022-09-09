using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ConsejoProfesionalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Profesion { get; set; }
        public string Calle { get; set; }
        public string NroPuerta { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public int? id_consejo_habilitaciones { get; set; }
        public int id_grupoconsejo { get; set; }

        public virtual GrupoConsejosDTO GrupoConsejosDTO { get; set; }

        public string Descripcion { get {
            return GrupoConsejosDTO.Descripcion;
        } }
    }
}
