using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public string cod_cli { get; set; }

        [Display(Name = "Nombre")]
        public string nom_cli { get; set; }

        [Display(Name = "Telefono")]
        public string tel_cli { get; set; }

        [Display(Name = "Correo")]
        public string cor_cli { get; set; }

        [Display(Name = "Crédito")]
        [Required, Range(0, 10000)]
        public string cred_cli {get; set;}

        [Display(Name = "Dirección")]
        public string dir_cli { get; set; }

        [Display(Name = "Distrito")]
        public int cod_dist { get; set; }

        [Display(Name = "Fecha Nac.")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fec_nac { get; set; }
        //
    }
}
