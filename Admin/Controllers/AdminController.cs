using Admin.Services.Api;
using Admin.ViewModels.Users;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Services;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISportPlusApiClient _apiClient;

        public AdminController(ISportPlusApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Index() => View();

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

            await _apiClient.CreateFieldAsync(model);
            TempData["SuccessMessage"] = "Đã thêm sân mới!";
            return RedirectToAction(nameof(Fields));
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
    }
}