using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("UserAccount")]
    public partial class UserAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(100)]
        public string username { get; set; }
        [Required]
        [StringLength(100)]
        public string pswd { get; set; }
        [StringLength(100)]
        public string roleid { get; set; }
        [StringLength(100)]
        public string kantor { get; set; }
        [StringLength(100)]
        public string layarlist { get; set; }
        [Required]
        [StringLength(100)]
        public string f_name { get; set; }
        [Required]
        [StringLength(100)]
        public string l_name { get; set; }
    }
}
