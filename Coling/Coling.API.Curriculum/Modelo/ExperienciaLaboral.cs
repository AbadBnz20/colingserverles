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
    public class ExperienciaLaboral:IExperienciaLaboral, ITableEntity
    {
        public string IdAfiliado { get; set; }
        public string IdInstitucion { get; set; }
        public string CargoTitulo { get; set; }
        public DateTime FachaInicio { get; set; }
        public DateTime FachaFinal { get; set; }
        public string Estado { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
