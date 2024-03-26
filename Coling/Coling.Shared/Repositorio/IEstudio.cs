using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared.Repositorio
{
    public interface IEstudio
    {
        public string IdTipoEstudio { get; set; }
        public string IdAfiliado { get; set; }
        public string IdInstitucion { get; set; }
        public string Anio { get; set; }
        public string Estado { get; set; }
    }
}
