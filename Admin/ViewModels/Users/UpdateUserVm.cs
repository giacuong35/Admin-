using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Users
{
    public class UpdateUserVm
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public int StatusId { get; set; } = 1;
    }
}