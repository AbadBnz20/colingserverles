using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface IExperienciaLaboralRepositorio
    {
        public Task<bool> Create(ExperienciaLaboral experiencia);
        public Task<bool> Update(ExperienciaLaboral experiencia);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<ExperienciaLaboral>> GetAll();
        public Task<ExperienciaLaboral> Get(string id);
    }
}
