using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IDireccionLogic
    {
        public Task<bool> InserterDireccion(Direccion direccion);
        public Task<bool> ModificarDireccion(Direccion direccion, int id);
        public Task<bool> EliminarDireccion(int id);
        public Task<List<Direccion>> ListarDireccion();
        public Task<Direccion> ObtenerDireccion(int id);
    }
}
