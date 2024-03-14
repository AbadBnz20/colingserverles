using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Telefono
    {
        [Key]
        public int Id { get; set; }
        public string NroTelefono { get; set; }
        public string Estado { get; set; }
        public int IdPersona { get; set; }
        [ForeignKey("IdPersona")]
        public virtual Persona persona { get; set; }
    }
}
