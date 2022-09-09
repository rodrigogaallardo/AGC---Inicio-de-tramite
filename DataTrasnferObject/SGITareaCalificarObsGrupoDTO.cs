using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SGITareaCalificarObsGrupoDTO
    {
        public int id_ObsGrupo { get; set; }
        public int id_tramitetarea { get; set; }
        public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUser { get; set; }
    }

    public class SGITareaCalificarObsGrupoGrillaDTO
    {
        public int id_ObsGrupo { get; set; }
        public string userApeNom { get; set; }
        public DateTime CreateDate { get; set; }
        public SGITareaCalificarObsDocsGrillaDTO SGITareaCalificarObsGrupo { get; set; }
    }
}


