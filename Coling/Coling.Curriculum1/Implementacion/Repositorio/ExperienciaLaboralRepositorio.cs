using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class ExperienciaLaboralRepositorio : IExperienciaLaboralRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;

        public ExperienciaLaboralRepositorio(IConfiguration conf)
        {
            // crear la variable de entorno
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "ExperienciaLaboral";
        }

        public async Task<bool> Create(ExperienciaLaboral experiencia)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(experiencia);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(string partitionkey, string rowkey)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.DeleteEntityAsync(partitionkey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ExperienciaLaboral> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"RowKey eq '{id}'";
            await foreach (ExperienciaLaboral experiencia in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                return experiencia;
            }
            return null;
        }

        public async Task<List<ExperienciaLaboral>> GetAll()
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"Estado eq 'Activo'";

            await foreach (ExperienciaLaboral experiencia in tablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(experiencia);
            }

            return lista;
        }

        public async Task<bool> Update(ExperienciaLaboral experiencia)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(experiencia, experiencia.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
