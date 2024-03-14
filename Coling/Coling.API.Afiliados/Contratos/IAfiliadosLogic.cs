using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IAfiliadosLogic
    {
        public Task<bool> InsertarAfiliado(Afiliado afiliado);
        public Task<bool> ModificarAfiliado(Afiliado afiliado, int id);
        public Task<bool> EliminarAfiliado(int id);
        public Task<List<Afiliado>> ListarAfiliados();
        public Task<Afiliado> ObtenerAfiliado(int id);
    }
}
