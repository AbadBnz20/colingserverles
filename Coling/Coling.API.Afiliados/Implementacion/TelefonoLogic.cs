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
    public class TelefonoLogic : ITelefonoLogic
    {
        private readonly Contexto contexto;
        public TelefonoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> EliminarTelefono(int id)
        {
            Telefono telefono = await contexto.Telefono.FirstOrDefaultAsync(x =>x.Id==id);
            contexto.Remove(telefono);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InserterTelefono(Telefono telefono)
        {
            contexto.Telefono.Add(telefono);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Telefono>> ListarTelefonos()
        {
            var lista = await contexto.Telefono.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarTelefono(Telefono telefono, int id)
        {
            Telefono telefonoM = await contexto.Telefono.FirstOrDefaultAsync(x => x.Id == id);
            if (telefonoM != null)
            {
                telefonoM.IdPersona = telefono.IdPersona;
                telefonoM.NroTelefono = telefono.NroTelefono;
                telefonoM.Estado=telefono.Estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Telefono> ObtenerTelefono(int id)
        {
            Telefono telefono = await contexto.Telefono.FirstOrDefaultAsync(x => x.Id == id);
            if (telefono != null)
            {
                return telefono;
            }
            return telefono;
        }
    }
}
