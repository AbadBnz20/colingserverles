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
    public class OfertaLaboralRepositorio : IOfertaLaboralRepositorio
    {

        private readonly IMongoCollection<OfertaLaboral> _oferta;

        public OfertaLaboralRepositorio(IConfiguration conf)
        {
            var connectionString = conf.GetSection("ConexionMongo").Value;
            var databaseName = "BolsadeTrabajo";
            var collectionName = "OfertaLaboral";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _oferta = database.GetCollection<OfertaLaboral>(collectionName);
        }


        public async Task<bool> Create(OfertaLaboral ofertaLaboral)
        {
            try
            {
                _oferta.InsertOne(ofertaLaboral);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var state = _oferta.DeleteOne(x => x.Id == id);
            return true;
        }

        public async Task<OfertaLaboral> Get(string id)
        {
            OfertaLaboral ofertaLaboral = _oferta.Find(x => x.Id == id).FirstOrDefault();
            return ofertaLaboral;
        }

        public async Task<List<OfertaLaboral>> GetAll()
        {
            var lista = await _oferta.Find(new BsonDocument()).ToListAsync();
            return lista;
        }

        public async Task<bool> Update(OfertaLaboral ofertaLaboral, string id)
        {
            _oferta.ReplaceOne(x => x.Id == id, ofertaLaboral);
            return true;
        }
    }
}
