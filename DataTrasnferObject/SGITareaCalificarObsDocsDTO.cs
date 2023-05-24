using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class SGITareaCalificarObsDocsDTO
    {
        public int id_ObsDocs { get; set; }
        public int id_ObsGrupo { get; set; }
        public int id_tdocreq { get; set; }
        public string Observacion_ObsDocs { get; set; }
        public string Respaldo_ObsDocs { get; set; }
        public int? id_file { get; set; }
        public int? id_certificado { get; set; }
        public bool Decido_no_subir { get; set; }
        public bool Actual { get; set; }
        public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUser { get; set; }
    }

    public class SGITareaCalificarObsDocsGrillaDTO
    {
        public int id_ObsGrupo { get; set; }
        public int id_master { get; set; }
        public int id_ObsDocs { get; set; }
        public string nombre_tdocreq { get; set; }
        public string Observacion_ObsDocs { get; set; }
        public string Respaldo_ObsDocs { get; set; }
        public int id_file { get; set; }
        public int id_certificado { get; set; }
        public bool Decido_no_subir { get; set; }
        public bool Actual { get; set; }
        public string cod_tipodocsis { get; set; }
        public string filename { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ObsDate { get; set; }
        public string Url { get; set; }
    }
}


