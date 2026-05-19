using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Admin.ViewModels.Fields
{
    public class CreateFieldVm
    {
        [Required(ErrorMessage = "Tên sân không được để trống")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Giá cơ bản không được để trống")]
        [Range(1000, double.MaxValue, ErrorMessage = "Giá cơ bản phải lớn hơn 0")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Giá cao điểm không được để trống")]
        [Range(1000, double.MaxValue, ErrorMessage = "Giá cao điểm phải lớn hơn 0")]
        public decimal PeakPrice { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int TypeId { get; set; } = 1;
        public int StatusId { get; set; } = 1;
    }

    public class EditFieldVm : CreateFieldVm
    {
    }
}