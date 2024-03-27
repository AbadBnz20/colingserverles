using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class EstudiosRepositorio : IEstudiosRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public EstudiosRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Estudio";
        }
        public async Task<bool> Create(Estudios estudio)
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tablaNombre);
                await tabla.UpsertEntityAsync(estudio);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string partitionkey, string rowkey)
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tablaNombre);
                await tabla.DeleteEntityAsync(partitionkey, rowkey);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Estudios> Get(string id)
        {
            var tabla = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"RowKey eq '{id}'";
            await foreach (Estudios estudio in tabla.QueryAsync<Estudios>(filter: filtro))
            {
                return estudio;
            }
            return null;
        }

        public async Task<List<Estudios>> GetAll()
        {
            List<Estudios> lista = new List<Estudios>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"Estado eq 'Activo'";

            await foreach (Estudios estudio in tablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                lista.Add(estudio);
            }

            return lista;
        }

        public async Task<bool> Update(Estudios estudio)
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tablaNombre);
                await tabla.UpdateEntityAsync(estudio, estudio.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
