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
    public class Profesion:IProfesion, ITableEntity

    {
        public string NombreProfesion { get; set; }
        public string idgrado { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
