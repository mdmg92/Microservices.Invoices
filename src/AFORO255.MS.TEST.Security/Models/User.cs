using System.ComponentModel.DataAnnotations.Schema;

namespace Security.Models
{
    [Table("users")]
    public class User
    {
        [Column("id_user")]
        public int Id { get; set; }
        
        [Column("username")]
        public string Username { get; set; }
        
        [Column("password")]
        public string Password { get; set; }
    }
}
