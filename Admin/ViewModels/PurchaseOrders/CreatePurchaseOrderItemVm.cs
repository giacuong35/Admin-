using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.PurchaseOrders
{
    public class CreatePurchaseOrderItemVm
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "Đơn giá phải lớn hơn 0")]
        public decimal UnitPrice { get; set; }
    }
}