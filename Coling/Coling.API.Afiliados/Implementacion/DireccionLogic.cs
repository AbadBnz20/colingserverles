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
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;
        public DireccionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

       public  async Task<bool> EliminarDireccion(int id)
        {
           Direccion direccion = await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Remove(direccion);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InserterDireccion(Direccion direccion)
        {
            contexto.Direccion.Add(direccion);
            int response =await contexto.SaveChangesAsync();
            if (response==1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            Direccion direccionM = await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
            if (direccionM != null)
            {
                direccionM.IdPersona = direccion.IdPersona;
                direccionM.direccion = direccion.direccion;
                direccionM.Estado= direccion.Estado;    
                await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Direccion>> ListarDireccion()
        {
            var lista = await contexto.Direccion.ToListAsync();
            return lista;
        }


        public async Task<Direccion> ObtenerDireccion(int id)
        {
            Direccion direccion = await contexto.Direccion.FirstOrDefaultAsync(x => x.Id == id);
            if (direccion !=null)
            {
                return direccion;
            }
            return direccion;
        }
    }
}
