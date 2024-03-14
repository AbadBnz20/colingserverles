using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersona(int id)
        {
            Persona per= await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            contexto.Remove(per);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool sw = false;
            contexto.Personas.Add(persona);
            int response=await contexto.SaveChangesAsync();
            if (response==1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarPersonaTodos()
        {
            var lista = await contexto.Personas.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarPersona(Persona persona, int id)
        {
            Persona per = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            if (per !=null)
            {
                per.Nombre = persona.Nombre;
                per.Apellidos=persona.Apellidos;
                per.FechaNacimiento = persona.FechaNacimiento;
                per.Foto= persona.Foto;
                per.Estado= persona.Estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Persona> ObtenerPersona(int id)
        {
            Persona persona = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            if(persona != null)
            {
                return persona;
            }
            return persona;
        }
    }
}
