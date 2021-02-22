using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion.Models
{
    public class Factura
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("empresa")]
        public string empresa { get; set; }

        [BsonElement("ciudad")]
        public string ciudad { get; set; }

        [BsonElement("nit")]
        public string nit { get; set; }

        [BsonElement("fechaCreacion")]
        public string fechaCreacion { get; set; }

        [BsonElement("estado")]
        public string estado { get; set; }

        [BsonElement("pago")]
        public bool pago { get; set; }

        [BsonElement("subtotal")]
        public int subtotal { get; set; }

        [BsonElement("iva")]
        public int iva { get; set; }

        [BsonElement("retencion")]
        public int retencion { get; set; }

        [BsonElement("total")]
        public int total { get; set; }

        [BsonElement("fechaPago")]
        public string fechaPago { get; set; }

    } 
}
