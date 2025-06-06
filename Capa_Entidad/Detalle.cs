using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Detalle
    {
        public string num_vta { get; set; }
        public string nom_cli { get; set; }
        public string nom_art { get; set; }
        public decimal pre_art { get; set; }
        public int cantidad { get; set; }
        public decimal total_producto
        {
            get
            {
                return pre_art * cantidad;
            }
        }
        public decimal tot_vta { get; set; }

    }
}
