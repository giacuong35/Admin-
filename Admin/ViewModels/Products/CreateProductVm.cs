using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Products
{
    public class CreateProductVm
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho ban đầu không được âm")]
        public int InitialStock { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Mức cảnh báo không được âm")]
        public int MinQty { get; set; } = 5;
    }
}