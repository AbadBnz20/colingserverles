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
    public class OfertaLaboral
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("IdInstitucion")]
        public string IdInstitucion { get; set; }
        [BsonElement("FechaOferta")]
        public DateTime FechaOferta { get; set; }
        [BsonElement("FechaLimite")]
        public DateTime FechaLimite { get; set; }
        [BsonElement("Descripcion")]
        public string Descripcion { get; set; }
        [BsonElement("TituloCargo")]
        public string TituloCargo { get; set; }
        [BsonElement("TipoContrato")]
        public string TipoContrato { get; set; }
        [BsonElement("TipoTrabajo")]
        public string TipoTrabajo { get; set; }
        [BsonElement("Area")]
        public string Area {  get; set; }
        [BsonElement("Caracteristicas")]
        public string[] Caracteristicas { get; set; }
        [BsonElement("Estado")]
        public string Estado {  get; set; }
    }
}
