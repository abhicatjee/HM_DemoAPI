using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace OnlineTestAPI.Entities
{
    public class User
    {
        [Key]
        [StringLength(5)]
        public string UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(30)]
        public string Role { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}
