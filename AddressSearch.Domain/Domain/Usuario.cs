using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Domain.Domain
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
        public DateTime DataInclusao { get; set; } = DateTime.Now;
    }
}
