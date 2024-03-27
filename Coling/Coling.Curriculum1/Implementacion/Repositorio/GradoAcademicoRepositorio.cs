using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class GradoAcademicoRepositorio: IGradoAcademicoRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public GradoAcademicoRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "GradoAcademico";
        }

       public  async Task<bool> Create(GradoAcademico grado)
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tablaNombre);
                await tabla.UpsertEntityAsync(grado);
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

        public async Task<GradoAcademico> Get(string id)
        {
            var tabla = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"RowKey eq '{id}'";
            await foreach (GradoAcademico grado in tabla.QueryAsync<GradoAcademico>(filter: filtro))
            {
                return grado;
            }
            return null;
        }

        public async Task<List<GradoAcademico>> GetAll()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"Estado eq 'Activo'";

            await foreach (GradoAcademico grado in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(grado);
            }

            return lista;
        }

        public async Task<bool> Update(GradoAcademico grado)
        {
            try
            {
                var tabla = new TableClient(cadenaConexion, tablaNombre);
                await tabla.UpdateEntityAsync(grado, grado.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
