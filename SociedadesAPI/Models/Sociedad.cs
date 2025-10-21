using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SociedadesAPI.Models
{
    [Table("Tbl_Sociedades")]
    public class Sociedad
    {
        [Key]
        public int Id {  get; set; }

        public string Consecutivo { get; set; } = "";

        public string ClaveCasfim { get; set; } = "";

        public string SociedadNombre { get; set; } = "";    

        public string CapitalNeto { get; set; } = "";   

        public string RequerimientoCapital { get; set; } = "";

        public string Nicap { get; set; } = "";

        public string Categoria { get; set; } = "";

        public string Federacion { get; set; } = "";
    }
}
