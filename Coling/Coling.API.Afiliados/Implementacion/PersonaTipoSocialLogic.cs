using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class PersonaTipoSocialLogic : IPersonaTipoSocial
    {
        private readonly Contexto contexto;
        public PersonaTipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersonaTipoSocial(int id)
        {
            PersonaTipoSocial ptiposocial = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Remove(ptiposocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personatipoSocial)
        {
            contexto.PersonaTipoSocial.Add(personatipoSocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                return true;
            }
            return false;
        }

        public  async Task<List<PersonaTipoSocial>> ListarPersonaTipoSocial()
        {
            var lista = await contexto.PersonaTipoSocial.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personatipoSocial, int id)
        {
            PersonaTipoSocial tipos = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tipos != null)
            {
                tipos.IdTipoSocial = personatipoSocial.IdTipoSocial;
                tipos.IdPersona=personatipoSocial.IdPersona;
                tipos.Estado= personatipoSocial.Estado;
                  await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<PersonaTipoSocial> ObtenerPersonaTipoSocial(int id)
        {
            PersonaTipoSocial tipos = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tipos != null)
            {
                return tipos;
            }
            return tipos;
        }
    }
}
