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
    public class TipoEstudioRepositorio: ITipoEstudioRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public TipoEstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "TipoEstudio";
        }

        public async Task<bool> Create(TipoEstudio estudio)
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

        public  async Task<TipoEstudio> Get(string id)
        {
            var tabla = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"RowKey eq '{id}'";
            await foreach (TipoEstudio estudio in tabla.QueryAsync<TipoEstudio>(filter: filtro))
            {
                return estudio;
            }
            return null;
        }

        public  async Task<List<TipoEstudio>> GetAll()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"Estado eq 'Activo'";

            await foreach (TipoEstudio estudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(estudio);
            }

            return lista;
        }

        public async Task<bool> Update(TipoEstudio estudio)
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
