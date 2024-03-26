using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared.Repositorio
{
    public interface IExperienciaLaboral
    {
        public string IdAfiliado { get; set; }
        public string IdInstitucion {  get; set; }
        public string CargoTitulo { get; set; }
        public DateTime FachaInicio { get; set; }
        public DateTime FachaFinal { get;set; }
        public string Estado { get; set; }
    }
}
