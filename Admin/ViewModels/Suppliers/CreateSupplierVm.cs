using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Suppliers
{
    public class CreateSupplierVm
    {
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên người liên hệ không được để trống")]
        [MaxLength(200)]
        public string ContactName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;
    }
}