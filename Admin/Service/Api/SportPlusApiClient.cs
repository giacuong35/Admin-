using Admin.ViewModels.Auth;
using Admin.ViewModels.Bookings;
using Admin.ViewModels.Common;
using Admin.ViewModels.Dashboard;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Invoices;
using Admin.ViewModels.Products;
using Admin.ViewModels.PurchaseOrders;
using Admin.ViewModels.Services;
using Admin.ViewModels.Suppliers;
using Admin.ViewModels.Users;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Admin.Services.Api
{
    public class SportPlusApiClient : ISportPlusApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SportPlusApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpClient CreateClient(bool withAuth = true)
        {
            var client = _httpClientFactory.CreateClient("SportPlusApi");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");

            if (withAuth && !string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        // =========================
        // AUTH
        // =========================
        public async Task<LoginResultVm?> LoginAsync(LoginVm model)
        {
            var client = CreateClient(false);

            var response = await client.PostAsJsonAsync("api/auth/login", new
            {
                identifier = model.Email,
                password = model.Password
            });

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<LoginResultVm>>();
            return result?.Data;
        }

        // =========================
        // USERS
        // =========================
        public async Task<List<UserListItemVm>> GetUsersAsync(int? roleId = null, int? statusId = null, string? search = null, int page = 1, int pageSize = 100)
        {
            var client = CreateClient();

            var query = $"api/users?page={page}&pageSize={pageSize}";

            if (roleId.HasValue)
                query += $"&roleId={roleId.Value}";

            if (statusId.HasValue)
                query += $"&statusId={statusId.Value}";

            if (!string.IsNullOrWhiteSpace(search))
                query += $"&search={Uri.EscapeDataString(search)}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<UserListItemVm>>>();
            return result?.Data?.Items ?? new List<UserListItemVm>();
        }

        public async Task<UserListItemVm?> GetUserByIdAsync(int userId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/users/{userId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<UserListItemVm>>();
            return result?.Data;
        }

        public async Task CreateStaffAsync(CreateStaffVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/users/staff", new
            {
                fullName = model.FullName,
                email = model.Email,
                phone = model.Phone,
                password = model.Password,
                statusId = 1
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task CreateCustomerAsync(CreateCustomerVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/users/customer", new
            {
                fullName = model.FullName,
                email = model.Email,
                phone = model.Phone,
                password = model.Password,
                statusId = 1
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync(int userId, UpdateUserVm model)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"api/users/{userId}", new
            {
                fullName = model.FullName,
                email = model.Email,
                phone = model.Phone,
                statusId = model.StatusId
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"api/users/{userId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task LockUserAsync(int userId)
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/users/{userId}/lock");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task UnlockUserAsync(int userId)
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/users/{userId}/unlock");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        // =========================
        // FIELDS
        // =========================
        public async Task<List<FieldListItemVm>> GetFieldsAsync()
        {
            var client = CreateClient();

            var response = await client.GetAsync("api/fields");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<FieldListItemVm>>>();
            return result?.Data?.Items ?? new List<FieldListItemVm>();
        }

        public async Task<FieldDetailVm?> GetFieldByIdAsync(int fieldId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/fields/{fieldId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<FieldDetailVm>>();
            return result?.Data;
        }

        public async Task CreateFieldAsync(CreateFieldVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/fields", new
            {
                name = model.Name,
                description = model.Description,
                basePrice = model.BasePrice,
                typeId = model.TypeId,
                statusId = model.StatusId
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateFieldAsync(int fieldId, EditFieldVm model)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"api/fields/{fieldId}", new
            {
                name = model.Name,
                description = model.Description,
                basePrice = model.BasePrice,
                typeId = model.TypeId,
                statusId = model.StatusId
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFieldAsync(int fieldId)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"api/fields/{fieldId}");
            response.EnsureSuccessStatusCode();
        }

        // =========================
        // SERVICES
        // =========================
        public async Task<List<ServiceListItemVm>> GetServicesAsync(bool? isAvailable = null)
        {
            var client = CreateClient();

            var url = "api/services";
            if (isAvailable.HasValue)
                url += $"?isAvailable={isAvailable.Value.ToString().ToLower()}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<List<ServiceListItemVm>>>();
            return result?.Data ?? new List<ServiceListItemVm>();
        }

        public async Task<ServiceListItemVm?> GetServiceByIdAsync(int serviceId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/services/{serviceId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<ServiceListItemVm>>();
            return result?.Data;
        }

        public async Task CreateServiceAsync(CreateServiceVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/services", new
            {
                name = model.Name,
                description = model.Description,
                price = model.Price,
                imageUrl = model.ImageUrl
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateServiceAsync(int serviceId, UpdateServiceVm model)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"api/services/{serviceId}", new
            {
                name = model.Name,
                description = model.Description,
                price = model.Price,
                imageUrl = model.ImageUrl,
                isAvailable = model.IsAvailable
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteServiceAsync(int serviceId)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"api/services/{serviceId}");
            response.EnsureSuccessStatusCode();
        }


        // =========================
        // BOOKINGS
        // =========================
        public async Task<PagedResult<BookingListItemVm>> GetBookingsAsync(
    int? userId = null,
    int? statusId = null,
    DateTime? dateFrom = null,
    DateTime? dateTo = null,
    int? fieldId = null,
    int page = 1,
    int pageSize = 20)
        {
            var client = CreateClient();

            var query = $"api/bookings?page={page}&pageSize={pageSize}";

            if (userId.HasValue)
                query += $"&userId={userId.Value}";

            if (statusId.HasValue)
                query += $"&statusId={statusId.Value}";

            if (dateFrom.HasValue)
                query += $"&dateFrom={dateFrom.Value:yyyy-MM-dd}";

            if (dateTo.HasValue)
                query += $"&dateTo={dateTo.Value:yyyy-MM-dd}";

            if (fieldId.HasValue)
                query += $"&fieldId={fieldId.Value}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<BookingListItemVm>>>();
            return result?.Data ?? new PagedResult<BookingListItemVm>();
        }

        public async Task<BookingDetailVm?> GetBookingByIdAsync(int bookingId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/bookings/{bookingId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<BookingDetailVm>>();
            return result?.Data;
        }

        public async Task CancelBookingAsync(int bookingId, CancelBookingVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync($"api/bookings/{bookingId}/cancel", new
            {
                reason = model.Reason
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task RecordBookingPaymentAsync(int bookingId, RecordBookingPaymentVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync($"api/bookings/{bookingId}/payment", new
            {
                methodId = model.MethodId,
                transactionCode = model.TransactionCode,
                note = model.Note
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task<List<PaymentVm>> GetBookingPaymentsAsync(int bookingId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/bookings/{bookingId}/payments");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<List<PaymentVm>>>();
            return result?.Data ?? new List<PaymentVm>();
        }

        public async Task<DepositVm?> GetBookingDepositAsync(int bookingId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/bookings/{bookingId}/deposit");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<DepositVm>>();
            return result?.Data;
        }


        public async Task CreateAdminWalkInBookingAsync(CreateAdminWalkInBookingVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/bookings/admin/walk-in", new
            {
                customerId = model.IsGuest ? null : model.CustomerId,
                isGuest = model.IsGuest,
                guestName = model.IsGuest
                    ? (string.IsNullOrWhiteSpace(model.GuestName) ? "Khách vãng lai" : model.GuestName)
                    : null,
                guestPhone = model.IsGuest ? model.GuestPhone : null,
                fieldSlotIds = model.SelectedSlotIds,
                services = new List<object>(),
                promotionCode = model.PromotionCode,
                note = model.Note,
                paymentOption = model.IsFullPayment ? 2 : 1,
                paymentMethodId = model.IsFullPayment ? model.PaymentMethodId : null,
                transactionCode = model.IsFullPayment ? model.TransactionCode : null
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task CompleteBookingAsync(int bookingId)
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/bookings/{bookingId}/complete");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetFieldScheduleRawAsync(DateTime date, int? fieldId = null, int? typeId = null)
        {
            var client = CreateClient();

            var query = $"api/fields/schedule?date={date:yyyy-MM-dd}";

            if (fieldId.HasValue)
                query += $"&fieldId={fieldId.Value}";

            if (typeId.HasValue)
                query += $"&typeId={typeId.Value}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        // =========================
        // DASHBOARD
        // =========================



        public async Task<DashboardSummaryVm?> GetDashboardSummaryAsync()
        {
            var client = CreateClient();

            var response = await client.GetAsync("api/dashboard/summary");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<DashboardSummaryVm>>();
            return result?.Data;
        }

        public async Task<List<RevenueByMonthVm>> GetRevenueByMonthAsync(int? year = null)
        {
            var client = CreateClient();

            var url = "api/dashboard/revenue-by-month";
            if (year.HasValue)
                url += $"?year={year.Value}";

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<List<RevenueByMonthVm>>>();
            return result?.Data ?? new List<RevenueByMonthVm>();
        }

        public async Task<List<FieldOccupancyVm>> GetFieldOccupancyAsync(int? year = null, int? month = null)
        {
            var client = CreateClient();

            var queries = new List<string>();
            if (year.HasValue) queries.Add($"year={year.Value}");
            if (month.HasValue) queries.Add($"month={month.Value}");

            var url = "api/dashboard/field-occupancy";
            if (queries.Any())
                url += "?" + string.Join("&", queries);

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<List<FieldOccupancyVm>>>();
            return result?.Data ?? new List<FieldOccupancyVm>();
        }

        public async Task<List<RevenueByServiceVm>> GetRevenueByServiceAsync()
        {
            var client = CreateClient();

            var response = await client.GetAsync("api/dashboard/revenue-by-service");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<List<RevenueByServiceVm>>>();
            return result?.Data ?? new List<RevenueByServiceVm>();
        }




        // =========================
        // SUPPLIERS
        // =========================

        public async Task<PagedResult<SupplierListItemVm>> GetSuppliersAsync(string? search = null, int page = 1, int pageSize = 20)
        {
            var client = CreateClient();

            var query = $"api/suppliers?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(search))
                query += $"&search={Uri.EscapeDataString(search)}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<SupplierListItemVm>>>();
            return result?.Data ?? new PagedResult<SupplierListItemVm>();
        }

        public async Task<SupplierListItemVm?> GetSupplierByIdAsync(int supplierId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/suppliers/{supplierId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<SupplierListItemVm>>();
            return result?.Data;
        }

        public async Task CreateSupplierAsync(CreateSupplierVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/suppliers", new
            {
                name = model.Name,
                contactName = model.ContactName,
                phone = model.Phone,
                email = model.Email,
                address = model.Address
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSupplierAsync(int supplierId, UpdateSupplierVm model)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"api/suppliers/{supplierId}", new
            {
                name = model.Name,
                contactName = model.ContactName,
                phone = model.Phone,
                email = model.Email,
                address = model.Address
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"api/suppliers/{supplierId}");
            response.EnsureSuccessStatusCode();
        }

        // =========================
        // Product
        // =========================


        public async Task<PagedResult<ProductListItemVm>> GetProductsAsync(string? search = null, bool? lowStockOnly = null, int page = 1, int pageSize = 20)
        {
            var client = CreateClient();

            var query = $"api/products?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(search))
                query += $"&search={Uri.EscapeDataString(search)}";
            if (lowStockOnly.HasValue)
                query += $"&lowStockOnly={lowStockOnly.Value.ToString().ToLower()}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<ProductListItemVm>>>();
            return result?.Data ?? new PagedResult<ProductListItemVm>();
        }

        public async Task<ProductListItemVm?> GetProductByIdAsync(int productId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/products/{productId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<ProductListItemVm>>();
            return result?.Data;
        }

        public async Task CreateProductAsync(CreateProductVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/products", new
            {
                name = model.Name,
                unit = model.Unit,
                initialStock = model.InitialStock,
                minQty = model.MinQty
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(int productId, UpdateProductVm model)
        {
            var client = CreateClient();

            var response = await client.PutAsJsonAsync($"api/products/{productId}", new
            {
                name = model.Name,
                unit = model.Unit,
                minQty = model.MinQty
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var client = CreateClient();

            var response = await client.DeleteAsync($"api/products/{productId}");
            response.EnsureSuccessStatusCode();
        }


        // =========================
        //PURCHASE ORDER
        // =========================


        public async Task<PagedResult<PurchaseOrderListItemVm>> GetPurchaseOrdersAsync(int? supplierId = null, int? statusId = null, int page = 1, int pageSize = 20)
        {
            var client = CreateClient();

            var query = $"api/purchase-orders?page={page}&pageSize={pageSize}";
            if (supplierId.HasValue)
                query += $"&supplierId={supplierId.Value}";
            if (statusId.HasValue)
                query += $"&statusId={statusId.Value}";

            var response = await client.GetAsync(query);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<PurchaseOrderListItemVm>>>();
            return result?.Data ?? new PagedResult<PurchaseOrderListItemVm>();
        }

        public async Task<PurchaseOrderDetailVm?> GetPurchaseOrderByIdAsync(int purchaseOrderId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/purchase-orders/{purchaseOrderId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PurchaseOrderDetailVm>>();
            return result?.Data;
        }

        public async Task CreatePurchaseOrderAsync(CreatePurchaseOrderVm model)
        {
            var client = CreateClient();

            var response = await client.PostAsJsonAsync("api/purchase-orders", new
            {
                supplierId = model.SupplierId,
                note = model.Note,
                items = model.Items.Select(x => new
                {
                    productId = x.ProductId,
                    quantity = x.Quantity,
                    unitPrice = x.UnitPrice
                }).ToList()
            });

            response.EnsureSuccessStatusCode();
        }

        public async Task ConfirmPurchaseOrderAsync(int purchaseOrderId)
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/purchase-orders/{purchaseOrderId}/confirm");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task CancelPurchaseOrderAsync(int purchaseOrderId)
        {
            var client = CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Patch, $"api/purchase-orders/{purchaseOrderId}/cancel");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }



        // =========================
        //  Invoices
        // =========================

        public async Task<List<InvoiceListItemVm>> GetInvoicesAsync(DateTime? date = null)
        {
            var client = CreateClient();
            var queryDate = (date ?? DateTime.Today).ToString("yyyy-MM-dd");

            var response = await client.GetAsync($"api/invoices?date={queryDate}&page=1&pageSize=100");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<PagedResult<InvoiceListItemVm>>>();
            return result?.Data?.Items ?? new List<InvoiceListItemVm>();
        }

        public async Task<InvoiceDetailVm?> GetInvoiceDetailAsync(int paymentId)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"api/invoices/{paymentId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiEnvelope<InvoiceDetailVm>>();
            return result?.Data;
        }
    }
}