using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    [Table("Kantor")]
    public partial class Kantor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int kantorid { get; set; }  
        public string namakantor { get; set; }
        public int isDef { get; set; }
    }
}
