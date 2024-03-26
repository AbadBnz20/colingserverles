using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class PersonaTipoSocial
    {
        public int Id { get; set; }
        public int IdTipoSocial {  get; set; }
        [ForeignKey("IdTipoSocial")]
        public virtual TipoSocial tiposocial { get; set; }
        public int IdPersona {  get; set; }
        [ForeignKey("IdPersona")]
        public virtual Persona persona { get; set; }
        public string Estado { get; set; }

    }
}
