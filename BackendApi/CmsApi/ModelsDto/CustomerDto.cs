using System.ComponentModel.DataAnnotations;

namespace CmsApi.ModelsDto
{
    public class CustomerDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int CreatedBy { get; set; }
    }
}
