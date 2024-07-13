
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsApi.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [StringLength(100)]
        public string? FirstName { get; set; }
        [StringLength(100)]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Customer(string firstName, string lastName, string email, string phone, string address, int createdBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedBy= createdBy;
            CreatedAt=DateTime.Now;

        }
        public static Customer CreateCustomer(string firstName, string lastName, string email, string phone, string address, int createdBy)
        {

            return new Customer(firstName, lastName, email, phone, address, createdBy);
        }
    }
}
