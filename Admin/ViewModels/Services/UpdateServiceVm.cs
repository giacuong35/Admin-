using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Services
{
    public class UpdateServiceVm
    {
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}