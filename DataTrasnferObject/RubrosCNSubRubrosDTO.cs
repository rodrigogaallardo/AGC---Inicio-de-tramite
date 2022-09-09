namespace DataTransferObject
{
    public class RubrosCNSubRubrosDTO
    {
        public int Id_rubroCNsubrubro { get; set; }
        public int Id_rubroCN { get; set; }
        public string Nombre { get; set; }
        public int IdGrupoCircuito { get; set; }
        public virtual RubrosCNDTO RubrosDTO { get; set; }
    }

    public class ItemRubrosCNSubRubrosDTO
    {
        public int Id_rubroCNsubrubro { get; set; }
        public int Id_rubroCN { get; set; }
        public string Nombre { get; set; }
        public int? IdGrupoCircuito { get; set; }
        public bool Enabled { get; set; }
        public virtual RubrosCNDTO RubrosDTO { get; set; }
    }
}
