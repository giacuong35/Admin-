using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Users
{
    public class CreateCustomerVm
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "123456";

        public int StatusId { get; set; } = 1;
    }
}