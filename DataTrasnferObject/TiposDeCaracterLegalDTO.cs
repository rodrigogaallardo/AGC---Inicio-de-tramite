using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class TiposDeCaracterLegalDTO
	{
		public int IdTipoCaracter { get; set; }
		public string CodTipoCaracter { get; set; }
		public string NomTipoCaracter { get; set; }
		public DateTime CreateDate { get; set; }
		public int DisponibilidadTipoCaracter { get; set; }
		public bool MuestraCargoTipoCaracter { get; set; }
	}				
}


