using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Afiliado
    {
        [Key]
        public int Id { get; set; }
        public int IdPersona { get; set; }
        [ForeignKey("IdPersona")]
        public virtual Persona persona { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public string CodigoAfiliado { get; set; }
        public int NroTituloProvicional { get; set; }
        public string Estado {  get; set; }

    }
}
