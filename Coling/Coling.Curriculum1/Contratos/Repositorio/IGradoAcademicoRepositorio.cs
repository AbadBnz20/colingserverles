using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface IGradoAcademicoRepositorio
    {
        public Task<bool> Create(GradoAcademico grado);
        public Task<bool> Update(GradoAcademico grado);
        public Task<bool> Delete(string partitionkey, string rowkey);
        public Task<List<GradoAcademico>> GetAll();
        public Task<GradoAcademico> Get(string id);
    }
}
