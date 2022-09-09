using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SGITramitesTareasTransferenciasDTO : ICloneable
	{
		public int Id { get; set; }
		public int IdTramiteTarea { get; set; }
		public int IdSolicitud { get; set; }

        public object Clone()
        {
            return new SGITramitesTareasTransferenciasDTO()
            {
                Id = this.Id,
                IdTramiteTarea = this.IdTramiteTarea,
                IdSolicitud = this.IdSolicitud
            };
        }
    }				
}


