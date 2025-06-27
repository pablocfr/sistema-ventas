using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class VENTAS_AGRUPADAS
    {
        [Display(Name = "N° Venta")]
        public string num_vta { get; set; }

        [Display(Name = "Cliente")]
        public string nom_cli { get; set; }

        [Display(Name = "Total Articulos")]
        public int total_articulos { get; set; }

        [Display(Name = "Monto de compra")]
        public decimal total_venta { get; set; }
    }
}
