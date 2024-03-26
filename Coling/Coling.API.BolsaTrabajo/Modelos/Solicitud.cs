using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.Modelos
{
    [BsonIgnoreExtraElements]
    public class Solicitud
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("idafiliado")]
        public string IdAfiliado { get; set; }
        [BsonElement("nombrecompleto")]
        public string NombreCompleto { get; set; }
        [BsonElement("fechapostulacion")]
        public DateTime FechaPostulacion { get; set; }
        [BsonElement("pretencionsalarial")]
        public int PretencionSalarial { get; set; }
        [BsonElement("acerdade")]
        public string Acerdade { get; set; }
        [BsonElement("idoferta")]
        public string IdOferta { get; set; }
    }
}
