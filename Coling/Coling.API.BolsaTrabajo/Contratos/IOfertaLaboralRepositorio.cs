using Coling.API.BolsaTrabajo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.Contratos
{
    public interface IOfertaLaboralRepositorio
    {
        public Task<bool> Create(OfertaLaboral ofertaLaboral);
        public Task<bool> Update(OfertaLaboral ofertaLaboral, string id);
        public Task<bool> Delete(string id);
        public Task<List<OfertaLaboral>> GetAll();
        public Task<OfertaLaboral> Get(string id);
    }
}
