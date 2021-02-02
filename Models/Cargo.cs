using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website___Ricardo.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }

        [Required(ErrorMessage = "Deve preencher o nome.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O nome deve ter pelo menos 4 caracteres e não deve exceder os 100 caracteres.")]
        public string Titulo { get; set; }
        [Display(Name = "Titulo")]

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        public byte [] Picture { get; set; }
    }

    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
    }
}
