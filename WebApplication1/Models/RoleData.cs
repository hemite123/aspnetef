using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("RoleData")]
    public partial class RoleData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int roleid { get; set; }
        public string namaRole { get;set; }
        [NotMapped]
        public bool isDefe { get;set }
        public int isDef { get;set;}
    }
}
