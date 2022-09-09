using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class SGITramitesTareasDTO : ICloneable
    {
        public int IdTramiteTarea { get; set; }
        public int IdTarea { get; set; }
        public int IdResultado { get; set; }
        public DateTime FechaInicioTramiteTarea { get; set; }
        public DateTime? FechaCierreTramiteTarea { get; set; }
        public Guid? UsuarioAsignadoTramiteTarea { get; set; }
        public DateTime? FechaAsignacionTramiteTarea { get; set; }
        public Guid? CreateUser { get; set; }
        public int? IdProximaTarea { get; set; }

        public object Clone()
        {
            return new SGITramitesTareasDTO()
            {
                IdTramiteTarea = this.IdTramiteTarea,
                IdTarea = this.IdTarea,
                IdResultado = this.IdResultado,
                FechaInicioTramiteTarea = this.FechaInicioTramiteTarea,
                FechaCierreTramiteTarea = this.FechaCierreTramiteTarea,
                UsuarioAsignadoTramiteTarea = this.UsuarioAsignadoTramiteTarea,
                FechaAsignacionTramiteTarea = this.FechaAsignacionTramiteTarea,
                CreateUser = this.CreateUser,
                IdProximaTarea = this.IdProximaTarea
            };
        }
    }
}


