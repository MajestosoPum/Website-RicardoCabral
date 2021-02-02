using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website___Ricardo.Models
{
    public class Empresa
    {
        public int EmpresaId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Empresa")]
        public string Nome { get; set; }

        public ICollection<Cargo> Cargos { get; set; }
    }
}
