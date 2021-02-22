using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCfacturacion.Models
{
    public class FacturaSettings : IFacturaSettings
    {
        public string server { get; set; }
        public string database { get; set; }
        public string collection { get; set; }
    }

    public interface IFacturaSettings
    {
        string server { get; set; }
        string database { get; set; }
        string collection { get; set; }
    }
}
