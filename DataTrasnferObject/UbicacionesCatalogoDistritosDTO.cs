﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class UbicacionesCatalogoDistritosDTO
    {
        public int IdDistrito { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdGrupoDistrito { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
