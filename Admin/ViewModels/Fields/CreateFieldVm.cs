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
        public decimal BasePrice { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int TypeId { get; set; } = 1;
        public int StatusId { get; set; } = 1;
    }

    public class EditFieldVm : CreateFieldVm
    {
    }
}