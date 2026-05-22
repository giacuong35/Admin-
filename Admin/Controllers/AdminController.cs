using Admin.Services.Api;
using Admin.ViewModels.Users;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;
using Admin.ViewModels.Bookings;
using Admin.ViewModels.Dashboard;
using Admin.ViewModels.Suppliers;
using Admin.ViewModels.Products;
using Admin.ViewModels.PurchaseOrders;
using Admin.ViewModels.Invoices;
using Admin.ViewModels.Incidents;

namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISportPlusApiClient _apiClient;

        public AdminController(ISportPlusApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            var vm = new DashboardPageVm
            {
                Summary = await _apiClient.GetDashboardSummaryAsync() ?? new DashboardSummaryVm(),
                RevenueByMonth = await _apiClient.GetRevenueByMonthAsync(currentYear),
                FieldOccupancy = await _apiClient.GetFieldOccupancyAsync(currentYear, currentMonth),
                RevenueByService = await _apiClient.GetRevenueByServiceAsync(),
                SelectedYear = currentYear,
                SelectedMonth = currentMonth
            };

            return View(vm);
        }

        public async Task<IActionResult> Reports(int? year, int? month)
        {
            var now = DateTime.Now;
            var selectedYear = year ?? now.Year;
            var selectedMonth = month ?? now.Month;

            var vm = new DashboardPageVm
            {
                Summary = await _apiClient.GetDashboardSummaryAsync() ?? new DashboardSummaryVm(),
                RevenueByMonth = await _apiClient.GetRevenueByMonthAsync(selectedYear),
                FieldOccupancy = await _apiClient.GetFieldOccupancyAsync(selectedYear, month),
                RevenueByService = await _apiClient.GetRevenueByServiceAsync(),
                MonthlyReport = await _apiClient.GetMonthlyReportAsync(selectedYear, selectedMonth) ?? new MonthlyReportVm
                {
                    Year = selectedYear,
                    Month = selectedMonth
                },
                SelectedYear = selectedYear,
                SelectedMonth = month
            };

            return View(vm);
        }

        // =========================
        // CUSTOMER
        // =========================
        public IActionResult CreateCustomer()
        {
            return View(new CreateCustomerVm { StatusId = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CreateCustomerVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.CreateCustomerAsync(model);
            TempData["SuccessMessage"] = "Đã thêm khách hàng mới thành công!";
            return RedirectToAction(nameof(Customers));
        }

        public async Task<IActionResult> Customers(int? statusId, DateTime? fromDate, DateTime? toDate)
        {
            var customerList = await _apiClient.GetUsersAsync(roleId: 3);

            if (statusId.HasValue)
                customerList = customerList.Where(x => x.StatusId == statusId.Value).ToList();

            if (fromDate.HasValue)
                customerList = customerList
                    .Where(x => x.CreatedAt.HasValue && x.CreatedAt.Value.Date >= fromDate.Value.Date)
                    .ToList();

            if (toDate.HasValue)
                customerList = customerList
                    .Where(x => x.CreatedAt.HasValue && x.CreatedAt.Value.Date <= toDate.Value.Date)
                    .ToList();

            ViewBag.StatusId = statusId;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(customerList);
        }

        public async Task<IActionResult> EditCustomer(int id)
        {
            var user = await _apiClient.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var vm = new UpdateUserVm
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone ?? string.Empty,
                StatusId = user.StatusId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, UpdateUserVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateUserAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật khách hàng!";
            return RedirectToAction(nameof(Customers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _apiClient.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "Đã xóa khách hàng!";
            return RedirectToAction(nameof(Customers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockCustomer(int id)
        {
            await _apiClient.LockUserAsync(id);
            TempData["SuccessMessage"] = "Đã khóa tài khoản khách hàng!";
            return RedirectToAction(nameof(Customers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockCustomer(int id)
        {
            await _apiClient.UnlockUserAsync(id);
            TempData["SuccessMessage"] = "Đã mở khóa tài khoản khách hàng!";
            return RedirectToAction(nameof(Customers));
        }

        // =========================
        // STAFF
        // =========================
        public IActionResult CreateStaff()
        {
            return View(new CreateStaffVm { StatusId = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStaff(CreateStaffVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.CreateStaffAsync(model);
            TempData["SuccessMessage"] = "Đã thêm nhân viên!";
            return RedirectToAction(nameof(Staff));
        }

        public async Task<IActionResult> Staff(int? statusId, DateTime? fromDate, DateTime? toDate, string? keyword)
        {
            var staffList = await _apiClient.GetUsersAsync(roleId: 2);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var kw = keyword.Trim().ToLower();
                staffList = staffList.Where(x =>
                    (!string.IsNullOrWhiteSpace(x.FullName) && x.FullName.ToLower().Contains(kw)) ||
                    (!string.IsNullOrWhiteSpace(x.Email) && x.Email.ToLower().Contains(kw)) ||
                    (!string.IsNullOrWhiteSpace(x.Phone) && x.Phone.ToLower().Contains(kw))
                ).ToList();
            }

            if (statusId.HasValue)
                staffList = staffList.Where(x => x.StatusId == statusId.Value).ToList();

            if (fromDate.HasValue)
                staffList = staffList
                    .Where(x => x.CreatedAt.HasValue && x.CreatedAt.Value.Date >= fromDate.Value.Date)
                    .ToList();

            if (toDate.HasValue)
                staffList = staffList
                    .Where(x => x.CreatedAt.HasValue && x.CreatedAt.Value.Date <= toDate.Value.Date)
                    .ToList();

            ViewBag.StatusId = statusId;
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");
            ViewBag.Keyword = keyword;

            return View(staffList);
        }

        public async Task<IActionResult> EditStaff(int id)
        {
            var user = await _apiClient.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var vm = new UpdateUserVm
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Phone = user.Phone ?? string.Empty,
                StatusId = user.StatusId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStaff(int id, UpdateUserVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateUserAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật nhân viên!";
            return RedirectToAction(nameof(Staff));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _apiClient.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "Đã xóa nhân viên!";
            return RedirectToAction(nameof(Staff));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockStaff(int id)
        {
            await _apiClient.LockUserAsync(id);
            TempData["SuccessMessage"] = "Đã khóa tài khoản nhân viên!";
            return RedirectToAction(nameof(Staff));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockStaff(int id)
        {
            await _apiClient.UnlockUserAsync(id);
            TempData["SuccessMessage"] = "Đã mở khóa tài khoản nhân viên!";
            return RedirectToAction(nameof(Staff));
        }

        // =========================
        // FIELDS
        // =========================
        public async Task<IActionResult> Fields()
        {
            var fields = await _apiClient.GetFieldsAsync();
            return View(fields);
        }

        public IActionResult CreateField()
        {
            return View(new CreateFieldVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateField(CreateFieldVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _apiClient.CreateFieldAsync(model);
                TempData["SuccessMessage"] = "Đã thêm sân mới!";
                return RedirectToAction(nameof(Fields));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> EditField(int id)
        {
            var field = await _apiClient.GetFieldByIdAsync(id);
            if (field == null) return NotFound();

            var vm = new EditFieldVm
            {
                Name = field.Name,
                Description = field.Description,
                BasePrice = field.BasePrice,
                TypeId = field.TypeId,
                StatusId = field.StatusId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditField(int id, EditFieldVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateFieldAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật sân!";
            return RedirectToAction(nameof(Fields));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteField(int id)
        {
            await _apiClient.DeleteFieldAsync(id);
            TempData["SuccessMessage"] = "Đã xóa sân!";
            return RedirectToAction(nameof(Fields));
        }

        // =========================
        // SERVICES
        // =========================
        public async Task<IActionResult> Services(bool? isAvailable)
        {
            var services = await _apiClient.GetServicesAsync(isAvailable);
            ViewBag.IsAvailable = isAvailable;
            return View(services);
        }

        public IActionResult CreateService()
        {
            return View(new CreateServiceVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(CreateServiceVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.CreateServiceAsync(model);
            TempData["SuccessMessage"] = "Đã thêm dịch vụ thành công!";
            return RedirectToAction(nameof(Services));
        }

        public async Task<IActionResult> EditService(int id)
        {
            var service = await _apiClient.GetServiceByIdAsync(id);
            if (service == null) return NotFound();

            var vm = new UpdateServiceVm
            {
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                ImageUrl = service.ImageUrl,
                IsAvailable = service.IsAvailable
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, UpdateServiceVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateServiceAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật dịch vụ thành công!";
            return RedirectToAction(nameof(Services));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteService(int id)
        {
            await _apiClient.DeleteServiceAsync(id);
            TempData["SuccessMessage"] = "Đã xóa dịch vụ thành công!";
            return RedirectToAction(nameof(Services));
        }

        // =========================
        // BOOKINGS
        // =========================
        public async Task<IActionResult> Bookings(int? userId, int? statusId, DateTime? dateFrom, DateTime? dateTo, int? fieldId, int page = 1)
        {
            var result = await _apiClient.GetBookingsAsync(userId, statusId, dateFrom, dateTo, fieldId, page, 20);

            ViewBag.UserId = userId;
            ViewBag.StatusId = statusId;
            ViewBag.DateFrom = dateFrom?.ToString("yyyy-MM-dd");
            ViewBag.DateTo = dateTo?.ToString("yyyy-MM-dd");
            ViewBag.FieldId = fieldId;

            return View(result);
        }

        public async Task<IActionResult> BookingDetail(int id)
        {
            var booking = await _apiClient.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            ViewBag.Payments = await _apiClient.GetBookingPaymentsAsync(id);
            ViewBag.Deposit = await _apiClient.GetBookingDepositAsync(id);

            return View(booking);
        }

        public IActionResult RecordBookingPayment(int id)
        {
            return View(new RecordBookingPaymentVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordBookingPayment(int id, RecordBookingPaymentVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.RecordBookingPaymentAsync(id, model);
            TempData["SuccessMessage"] = "Đã ghi nhận thanh toán booking!";
            return RedirectToAction(nameof(BookingDetail), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBooking(int id, CancelBookingVm model)
        {
            await _apiClient.CancelBookingAsync(id, model);
            TempData["SuccessMessage"] = "Đã hủy booking!";
            return RedirectToAction(nameof(BookingDetail), new { id });
        }

        public async Task<IActionResult> CreateAdminWalkInBooking()
        {
            var customersResult = await _apiClient.GetUsersAsync(search: null, roleId: null, statusId: null, page: 1, pageSize: 100);

            ViewBag.Customers = customersResult
                .Where(x => x.RoleId == 3 && x.StatusId == 1)
                .ToList();

            return View(new CreateAdminWalkInBookingVm
            {
                BookingDate = DateTime.Today
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetWalkInSchedule(DateTime date, int? fieldId = null, int? typeId = null)
        {
            var json = await _apiClient.GetFieldScheduleRawAsync(date, fieldId, typeId);
            return Content(json, "application/json");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdminWalkInBooking(CreateAdminWalkInBookingVm model)
        {
            if (!model.IsGuest && !model.CustomerId.HasValue)
            {
                ModelState.AddModelError("", "Vui lòng chọn khách hàng.");
            }

            if (model.IsGuest && string.IsNullOrWhiteSpace(model.GuestName))
            {
                model.GuestName = "Khách vãng lai";
            }

            if (model.SelectedSlotIds == null || !model.SelectedSlotIds.Any())
            {
                ModelState.AddModelError("", "Vui lòng chọn ít nhất 1 khung giờ.");
            }

            if (model.SelectedSlotIds.Count > 3)
            {
                ModelState.AddModelError("", "Chỉ được chọn tối đa 3 khung giờ.");
            }
                
            if (model.IsFullPayment && !model.PaymentMethodId.HasValue)
            {
                ModelState.AddModelError("", "Vui lòng chọn phương thức thanh toán.");
            }

            if (!ModelState.IsValid)
            {
                var customersResult = await _apiClient.GetUsersAsync(search: null, roleId: null, statusId: null, page: 1, pageSize: 100);

                ViewBag.Customers = customersResult
                    .Where(x => x.RoleId == 3 && x.StatusId == 1)
                    .ToList();

                return View(model);
            }

            await _apiClient.CreateAdminWalkInBookingAsync(model);

            TempData["SuccessMessage"] = "Đặt sân tại quầy thành công!";
            return RedirectToAction(nameof(Bookings));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteBooking(int id)
        {
            await _apiClient.CompleteBookingAsync(id);
            TempData["SuccessMessage"] = "Đã hoàn thành booking!";
            return RedirectToAction(nameof(BookingDetail), new { id });
        }

        public async Task<IActionResult> AdminRescheduleBooking(int bookingId, int bookingDetailId)
        {
            var booking = await _apiClient.GetBookingByIdAsync(bookingId);
            if (booking == null) return NotFound();

            var targetDetail = booking.Details?.FirstOrDefault(x => x.BookingDetailId == bookingDetailId);
            if (targetDetail == null) return NotFound();

            ViewBag.Booking = booking;
            ViewBag.TargetDetail = targetDetail;

            return View(new AdminRescheduleBookingVm
            {
                BookingId = bookingId,
                BookingDetailId = bookingDetailId,
                SelectedDate = targetDetail.SlotDate
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRescheduleBooking(AdminRescheduleBookingVm model)
        {
            var booking = await _apiClient.GetBookingByIdAsync(model.BookingId);
            if (booking == null) return NotFound();

            var targetDetail = booking.Details?.FirstOrDefault(x => x.BookingDetailId == model.BookingDetailId);
            if (targetDetail == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Booking = booking;
                ViewBag.TargetDetail = targetDetail;
                return View(model);
            }

            try
            {
                await _apiClient.AdminRescheduleBookingAsync(model.BookingId, model);

                TempData["SuccessMessage"] = "Đổi lịch booking thành công!";
                return RedirectToAction(nameof(BookingDetail), new { id = model.BookingId });
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (message.Contains("đạt số lần đổi lịch tối đa", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", "Booking này đã đạt số lần đổi lịch tối đa, không thể đổi thêm.");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể đổi lịch lúc này. Vui lòng thử lại.");
                }

                ViewBag.Booking = booking;
                ViewBag.TargetDetail = targetDetail;
                return View(model);
            }
        }


        // =========================
        // SUPPLIERS
        // =========================
        public async Task<IActionResult> Suppliers(string? search, int page = 1)
        {
            string? keyword = search?.Trim();
            bool isSupplierCodeSearch = false;
            int supplierId = 0;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var normalized = keyword.ToLower()
                    .Replace("sup-", "")
                    .Replace("sup", "")
                    .Trim();

                if (int.TryParse(normalized, out supplierId))
                {
                    isSupplierCodeSearch = true;
                }
            }

            // Nếu là tìm theo mã NCC thì không truyền search lên API
            var result = await _apiClient.GetSuppliersAsync(
                isSupplierCodeSearch ? null : keyword,
                1,
                200
            );

            if (isSupplierCodeSearch)
            {
                result.Items = result.Items
                    .Where(x => x.SupplierId == supplierId)
                    .ToList();

                result.TotalCount = result.Items.Count;
                result.Page = 1;
                result.PageSize = result.Items.Count == 0 ? 1 : result.Items.Count;
                result.TotalPages = result.Items.Count > 0 ? 1 : 0;
                result.HasNextPage = false;
                result.HasPreviousPage = false;
            }

            ViewBag.Search = search;
            return View(result);
        }

        public IActionResult CreateSupplier()
        {
            return View(new CreateSupplierVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSupplier(CreateSupplierVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.CreateSupplierAsync(model);
            TempData["SuccessMessage"] = "Đã thêm nhà cung cấp thành công!";
            return RedirectToAction(nameof(Suppliers));
        }

        public async Task<IActionResult> EditSupplier(int id)
        {
            var supplier = await _apiClient.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();

            var vm = new UpdateSupplierVm
            {
                Name = supplier.Name,
                ContactName = supplier.ContactName,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSupplier(int id, UpdateSupplierVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateSupplierAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật nhà cung cấp thành công!";
            return RedirectToAction(nameof(Suppliers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await _apiClient.DeleteSupplierAsync(id);
            TempData["SuccessMessage"] = "Đã xóa nhà cung cấp thành công!";
            return RedirectToAction(nameof(Suppliers));
        }

        // =========================
        // PRODUCTS / INVENTORY
        // =========================
        public async Task<IActionResult> Products(string? search, bool? lowStockOnly, int page = 1)
        {
            var result = await _apiClient.GetProductsAsync(search, lowStockOnly, page, 20);
            ViewBag.Search = search;
            ViewBag.LowStockOnly = lowStockOnly;
            return View(result);
        }

        public IActionResult CreateProduct()
        {
            return View(new CreateProductVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.CreateProductAsync(model);
            TempData["SuccessMessage"] = "Đã thêm sản phẩm kho thành công!";
            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _apiClient.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var vm = new UpdateProductVm
            {
                Name = product.Name,
                Unit = product.Unit,
                MinQty = product.MinQty
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, UpdateProductVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _apiClient.UpdateProductAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật sản phẩm kho thành công!";
            return RedirectToAction(nameof(Products));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _apiClient.DeleteProductAsync(id);
            TempData["SuccessMessage"] = "Đã xóa sản phẩm kho thành công!";
            return RedirectToAction(nameof(Products));
        }


        // =========================
        // PURCHASE ORDERS
        // =========================
        public async Task<IActionResult> PurchaseOrders(int? supplierId, int? statusId, int page = 1)
        {
            var result = await _apiClient.GetPurchaseOrdersAsync(supplierId, statusId, page, 20);
            ViewBag.SupplierId = supplierId;
            ViewBag.StatusId = statusId;
            return View(result);
        }

        public async Task<IActionResult> PurchaseOrderDetail(int id)
        {
            var order = await _apiClient.GetPurchaseOrderByIdAsync(id);
            if (order == null) return NotFound();

            return View(order);
        }

        public async Task<IActionResult> CreatePurchaseOrder()
        {
            ViewBag.Suppliers = (await _apiClient.GetSuppliersAsync(null, 1, 200)).Items;
            ViewBag.Products = (await _apiClient.GetProductsAsync(null, null, 1, 500)).Items;
            return View(new CreatePurchaseOrderVm
            {
                Items = new List<CreatePurchaseOrderItemVm>
        {
            new CreatePurchaseOrderItemVm()
        }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePurchaseOrder(CreatePurchaseOrderVm model)
        {
            if (model.Items == null || !model.Items.Any())
            {
                ModelState.AddModelError("", "Phải có ít nhất 1 sản phẩm.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Suppliers = (await _apiClient.GetSuppliersAsync(null, 1, 200)).Items;
                ViewBag.Products = (await _apiClient.GetProductsAsync(null, null, 1, 500)).Items;
                return View(model);
            }

            await _apiClient.CreatePurchaseOrderAsync(model);
            TempData["SuccessMessage"] = "Đã tạo đơn nhập kho thành công!";
            return RedirectToAction(nameof(PurchaseOrders));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmPurchaseOrder(int id)
        {
            await _apiClient.ConfirmPurchaseOrderAsync(id);
            TempData["SuccessMessage"] = "Đã xác nhận nhập kho thành công!";
            return RedirectToAction(nameof(PurchaseOrderDetail), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelPurchaseOrder(int id)
        {
            await _apiClient.CancelPurchaseOrderAsync(id);
            TempData["SuccessMessage"] = "Đã hủy đơn nhập kho!";
            return RedirectToAction(nameof(PurchaseOrderDetail), new { id });
        }


        // =========================
        // Invoices
        // =========================

        public async Task<IActionResult> Invoices(DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Today;

            var vm = new InvoicePageVm
            {
                Date = targetDate,
                Items = await _apiClient.GetInvoicesAsync(targetDate)
            };

            return View(vm);
        }

        public async Task<IActionResult> InvoiceDetail(int id)
        {
            var invoice = await _apiClient.GetInvoiceDetailAsync(id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        public async Task<IActionResult> InvoicePreview(int id)
        {
            var invoice = await _apiClient.GetInvoiceDetailAsync(id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        public async Task<IActionResult> PrintInvoicePdf(int id)
        {
            var pdfBytes = await _apiClient.DownloadInvoicePdfAsync(id);

            if (pdfBytes == null || pdfBytes.Length == 0)
                return NotFound();

            return File(pdfBytes, "application/pdf", $"HoaDon_{id}.pdf");
        }


        // =========================
        // INCIDENTS
        // =========================
        public async Task<IActionResult> Incidents(int? fieldId, int? statusId)
        {
            var items = await _apiClient.GetIncidentsAsync(fieldId, statusId, 1, 100);
            ViewBag.FieldId = fieldId;
            ViewBag.StatusId = statusId;
            ViewBag.Fields = await _apiClient.GetFieldsAsync();
            return View(items);
        }

        public async Task<IActionResult> IncidentDetail(int id)
        {
            var item = await _apiClient.GetIncidentByIdAsync(id);
            if (item == null) return NotFound();

            return View(item);
        }

        public async Task<IActionResult> CreateIncident()
        {
            ViewBag.Fields = await _apiClient.GetFieldsAsync();
            return View(new CreateIncidentVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIncident(CreateIncidentVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Fields = await _apiClient.GetFieldsAsync();
                return View(model);
            }

            await _apiClient.CreateIncidentAsync(model);
            TempData["SuccessMessage"] = "Đã tạo sự cố mới!";
            return RedirectToAction(nameof(Incidents));
        }

        public async Task<IActionResult> HandleIncident(int id)
        {
            var item = await _apiClient.GetIncidentByIdAsync(id);
            if (item == null) return NotFound();

            ViewBag.Incident = item;

            return View(new HandleIncidentVm
            {
                StatusId = item.StatusId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandleIncident(int id, HandleIncidentVm model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Incident = await _apiClient.GetIncidentByIdAsync(id);
                return View(model);
            }

            await _apiClient.HandleIncidentAsync(id, model);
            TempData["SuccessMessage"] = "Đã cập nhật xử lý sự cố!";
            return RedirectToAction(nameof(IncidentDetail), new { id });
        }
    }
}