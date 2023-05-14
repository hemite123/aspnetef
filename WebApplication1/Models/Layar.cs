using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    [Table("Layar")]
    public partial class Layar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int layarid { get; set; }
        public string namalayar { get; set; }
        [NotMapped]
        public bool boolisview { get; set; }
        public int isDef { get; set; }  
        public string pathcode { get; set; }
        public int roleNeed { get; set; }
    }
}
