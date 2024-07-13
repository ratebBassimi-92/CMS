using CmsApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }

        [Required] 
        public UserType userType { get; set; }

        public string HashPassword { get; set; }
        public string SaltPassword { get; set; }

        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User() { }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public static User CreateUser(string userName, string password)
        {
            return new User(userName, password);
        }
    }
}
