using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.PurchaseOrders
{
    public class CreatePurchaseOrderVm
    {
        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int SupplierId { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [MinLength(1, ErrorMessage = "Phải có ít nhất 1 sản phẩm")]
        public List<CreatePurchaseOrderItemVm> Items { get; set; } = new();
    }
}