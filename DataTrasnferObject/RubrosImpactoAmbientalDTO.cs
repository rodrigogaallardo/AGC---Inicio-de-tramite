using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class RubrosImpactoAmbientalDTO
	{
		public int IdRubroImpactoAmbiental { get; set; }
		public int IdRubro { get; set; }
		public int IdImpactoAmbiental { get; set; }
		public decimal DesdeM2 { get; set; }
		public decimal HastaM2 { get; set; }
		public bool AntenaEmisora { get; set; }
		public string LetraAnexo { get; set; }
	}				
}


