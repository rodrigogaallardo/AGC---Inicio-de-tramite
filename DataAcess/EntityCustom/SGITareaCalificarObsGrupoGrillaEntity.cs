﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class SGITareaCalificarObsGrupoGrillaEntity
    {
        public int id_ObsGrupo { get; set; }
        public string userApeNom { get; set; }
        public DateTime CreateDate { get; set; }

        public SGITareaCalificarObsGrillaEntity Observaciones { get; set; }
    }
}
