using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface ITipoEstudioRepositorio
    {
        public Task<bool> Create(TipoEstudio estudio);
        public Task<bool> Update(TipoEstudio estudio);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<TipoEstudio>> GetAll();
        public Task<TipoEstudio> Get(string id);
    }
}
