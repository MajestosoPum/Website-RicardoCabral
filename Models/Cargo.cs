using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website___Ricardo.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public string Titulo { get; set; }

        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
    }

    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
    }
}
