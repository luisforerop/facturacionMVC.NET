using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("empresa")]
        public string empresa { get; set; }

        [BsonElement("nit")]
        public string nit { get; set; }

        [BsonElement("ciudad")]
        public string ciudad { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("encargado")]
        public string encargado { get; set; }

    }




}
