using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion.Models
{
    public class infoEmail
    {
        public string emailOrigen { get; set; } 
        public string emailDestino {get; set;} 
        public string key {get; set;} 
        public string asunto {get; set;} 
        public string mensaje {get; set;} 
    }
}
