using Admin.ViewModels.Auth;
using Admin.ViewModels.Bookings;
using Admin.ViewModels.Common;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Services;
using Admin.ViewModels.Users;
using Admin.ViewModels.Dashboard;
using Admin.ViewModels.Suppliers;
using Admin.ViewModels.Products;
using Admin.ViewModels.PurchaseOrders;
using Admin.ViewModels.Invoices;
using Admin.ViewModels.Incidents;


namespace Admin.Services.Api
{
    public interface ISportPlusApiClient
    {
        Task<LoginResultVm?> LoginAsync(LoginVm model);

        // Users
        Task<List<UserListItemVm>> GetUsersAsync(int? roleId = null, int? statusId = null, string? search = null, int page = 1, int pageSize = 100);
        Task<UserListItemVm?> GetUserByIdAsync(int userId);
        Task CreateStaffAsync(CreateStaffVm model);
        Task CreateCustomerAsync(CreateCustomerVm model);
        Task UpdateUserAsync(int userId, UpdateUserVm model);
        Task DeleteUserAsync(int userId);
        Task LockUserAsync(int userId);
        Task UnlockUserAsync(int userId);

        // Fields
        Task<List<FieldListItemVm>> GetFieldsAsync();
        Task<FieldDetailVm?> GetFieldByIdAsync(int fieldId);
        Task CreateFieldAsync(CreateFieldVm model);
        Task UpdateFieldAsync(int fieldId, EditFieldVm model);
        Task DeleteFieldAsync(int fieldId);

        // Services
        Task<List<ServiceListItemVm>> GetServicesAsync(bool? isAvailable = null);
        Task<ServiceListItemVm?> GetServiceByIdAsync(int serviceId);
        Task CreateServiceAsync(CreateServiceVm model);
        Task UpdateServiceAsync(int serviceId, UpdateServiceVm model);
        Task DeleteServiceAsync(int serviceId);

        // Bookings
        Task<PagedResult<BookingListItemVm>> GetBookingsAsync(
            int? userId = null,
            int? statusId = null,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int? fieldId = null,
            int page = 1,
            int pageSize = 20);

        Task<BookingDetailVm?> GetBookingByIdAsync(int bookingId);
        Task CancelBookingAsync(int bookingId, CancelBookingVm model);
        Task RecordBookingPaymentAsync(int bookingId, RecordBookingPaymentVm model);
        Task<List<PaymentVm>> GetBookingPaymentsAsync(int bookingId);
        Task<DepositVm?> GetBookingDepositAsync(int bookingId);
        Task CreateAdminWalkInBookingAsync(CreateAdminWalkInBookingVm model);
        Task<string> GetFieldScheduleRawAsync(DateTime date, int? fieldId = null, int? typeId = null);
        Task CompleteBookingAsync(int bookingId);
        Task AdminRescheduleBookingAsync(int bookingId, AdminRescheduleBookingVm model);


        // Dashboard
        Task<DashboardSummaryVm?> GetDashboardSummaryAsync();
        Task<List<RevenueByMonthVm>> GetRevenueByMonthAsync(int? year = null);
        Task<List<FieldOccupancyVm>> GetFieldOccupancyAsync(int? year = null, int? month = null);
        Task<List<RevenueByServiceVm>> GetRevenueByServiceAsync();
        Task<MonthlyReportVm?> GetMonthlyReportAsync(int year, int? month = null);


        // Suppliers
        Task<PagedResult<SupplierListItemVm>> GetSuppliersAsync(string? search = null, int page = 1, int pageSize = 20);
        Task<SupplierListItemVm?> GetSupplierByIdAsync(int supplierId);
        Task CreateSupplierAsync(CreateSupplierVm model);
        Task UpdateSupplierAsync(int supplierId, UpdateSupplierVm model);
        Task DeleteSupplierAsync(int supplierId);



        // Products / Inventory
        Task<PagedResult<ProductListItemVm>> GetProductsAsync(string? search = null, bool? lowStockOnly = null, int page = 1, int pageSize = 20);
        Task<ProductListItemVm?> GetProductByIdAsync(int productId);
        Task CreateProductAsync(CreateProductVm model);
        Task UpdateProductAsync(int productId, UpdateProductVm model);
        Task DeleteProductAsync(int productId);


        // Purchase Orders
        Task<PagedResult<PurchaseOrderListItemVm>> GetPurchaseOrdersAsync(int? supplierId = null, int? statusId = null, int page = 1, int pageSize = 20);
        Task<PurchaseOrderDetailVm?> GetPurchaseOrderByIdAsync(int purchaseOrderId);
        Task CreatePurchaseOrderAsync(CreatePurchaseOrderVm model);
        Task ConfirmPurchaseOrderAsync(int purchaseOrderId);
        Task CancelPurchaseOrderAsync(int purchaseOrderId);

        //Invoices
        Task<List<InvoiceListItemVm>> GetInvoicesAsync(DateTime? date = null);
        Task<InvoiceDetailVm?> GetInvoiceDetailAsync(int paymentId);
        Task<byte[]?> DownloadInvoicePdfAsync(int paymentId);
        Task<string?> GetInvoicePdfUrlAsync(int paymentId);


        // Incidents
        Task<List<IncidentListItemVm>> GetIncidentsAsync(int? fieldId = null, int? statusId = null, int page = 1, int pageSize = 50);
        Task<IncidentDetailVm?> GetIncidentByIdAsync(int incidentId);
        Task CreateIncidentAsync(CreateIncidentVm model);
        Task HandleIncidentAsync(int incidentId, HandleIncidentVm model);
    }
}