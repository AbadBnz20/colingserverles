using Coling.API.BolsaTrabajo.Contratos;
using Coling.API.BolsaTrabajo.Modelos;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.Implementacion
{
    public class SolicitudRepositorio : ISolicitudRepositorio
    {

        private readonly IMongoCollection<Solicitud> _solicitud;

        public SolicitudRepositorio(IConfiguration conf)
        {
            var connectionString = conf.GetSection("ConexionMongo").Value;
            var databaseName = "BolsadeTrabajo";
            var collectionName = "Solicitud";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _solicitud = database.GetCollection<Solicitud>(collectionName);
        }

        public async Task<bool> Create(Solicitud solicitud)
        {
            try
            {
                _solicitud.InsertOne(solicitud);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var state = _solicitud.DeleteOne(x => x.Id == id);
            return true;
        }

        public async Task<Solicitud> Get(string id)
        {
            Solicitud solicitud =  _solicitud.Find(x=>x.Id==id).FirstOrDefault();
            return solicitud;
        }

        public async Task<List<Solicitud>> GetAll()
        {
            var lista = await _solicitud.Find(new BsonDocument()).ToListAsync();
            return lista;
        }

        public async Task<bool> Update(Solicitud solicitud, string id)
        {
            _solicitud.ReplaceOne(x => x.Id == id, solicitud);
            return true;
        }
    }
}
