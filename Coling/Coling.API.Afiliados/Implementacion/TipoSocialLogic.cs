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
    public class TipoSocialLogic : ITipoSocialLogic
    {
        private readonly Contexto contexto;
        public TipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTipoSocial(int id)
        {
            TipoSocial tiposocial = await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Remove(tiposocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipoSocial)
        {
            contexto.TipoSocial.Add(tipoSocial);
            int response =await contexto.SaveChangesAsync();
            if (response==1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<TipoSocial>> ListarTipoSocial()
        {
            var lista = await contexto.TipoSocial.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id)
        {
            TipoSocial tipos = await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tipos != null)
            {
                tipos.NombreSocial = tipoSocial.NombreSocial;
                tipos.Estado=tipoSocial.Estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<TipoSocial> ObtenerTipoSocial(int id)
        {
            TipoSocial tipos = await contexto.TipoSocial.FirstOrDefaultAsync(x => x.Id == id);
            if (tipos != null)
            {
                return tipos;
            }
            return tipos;
        }
    }
}
