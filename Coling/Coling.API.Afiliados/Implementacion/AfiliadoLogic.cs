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
    public class AfiliadoLogic : IAfiliadosLogic
    {
        private readonly Contexto contexto;
        public AfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliado(int id)
        {
            Afiliado afiliado = await contexto.Afiliado.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Remove(afiliado);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
            contexto.Afiliado.Add(afiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Afiliado>> ListarAfiliados()
        {
            var lista = await contexto.Afiliado.ToListAsync();
            return lista;
        }

        public  async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            Afiliado afiliadom = await contexto.Afiliado.FirstOrDefaultAsync(x => x.Id == id);
            if (afiliado != null)
            {
                afiliadom.IdPersona = afiliado.IdPersona;
                afiliadom.FechaAfiliacion = afiliado.FechaAfiliacion;
                afiliadom.CodigoAfiliado=afiliado.CodigoAfiliado;
                afiliadom.NroTituloProvicional = afiliado.NroTituloProvicional;
                afiliadom.Estado=afiliado.Estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Afiliado> ObtenerAfiliado(int id)
        {
            Afiliado afiliados = await contexto.Afiliado.FirstOrDefaultAsync(x => x.Id == id);
            if (afiliados != null)
            {
                return afiliados;
            }
            return afiliados;
        }
    }
}
