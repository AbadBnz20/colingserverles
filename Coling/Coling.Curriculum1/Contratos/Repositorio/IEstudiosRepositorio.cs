using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface IEstudiosRepositorio
    {
        public Task<bool> Create(Estudios estudio);
        public Task<bool> Update(Estudios estudio);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<Estudios>> GetAll();
        public Task<Estudios> Get(string id);
    }
}
