//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ENG_Rel_Resultados_Tareas_Transiciones
    {
        public int id_resultadotareatransicion { get; set; }
        public int id_resultadotarea { get; set; }
        public int id_transicion { get; set; }
    
        public virtual ENG_Rel_Resultados_Tareas ENG_Rel_Resultados_Tareas { get; set; }
        public virtual ENG_Transiciones ENG_Transiciones { get; set; }
    }
}