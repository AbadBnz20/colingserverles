using Azure;
using Azure.Data.Tables;
using Coling.Shared.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Modelo
{
    public class Estudios:IEstudio, ITableEntity
    {
        public string IdTipoEstudio { get; set; }
        public string IdAfiliado { get; set; }
        public string IdInstitucion { get; set; }
        public string Anio { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
