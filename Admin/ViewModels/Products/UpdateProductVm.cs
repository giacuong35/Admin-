using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Products
{
    public class UpdateProductVm
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Unit { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Mức cảnh báo không được âm")]
        public int? MinQty { get; set; }
    }
}