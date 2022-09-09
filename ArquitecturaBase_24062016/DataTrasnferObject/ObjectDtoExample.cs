using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTrasnferObject
{
    /// <summary>
    /// Usar ID como Clave principal
    /// </summary>
    public class ObjectDtoExample
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
        public string MyProperty2 { get; set; }
        public Decimal MyProperty3 { get; set; }
    }
}
