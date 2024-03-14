using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IPersonaTipoSocial
    {
        public Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personatipoSocial);
        public Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personatipoSocial, int id);
        public Task<bool> EliminarPersonaTipoSocial(int id);
        public Task<List<PersonaTipoSocial>> ListarPersonaTipoSocial();
        public Task<PersonaTipoSocial> ObtenerPersonaTipoSocial(int id);
    }
}
