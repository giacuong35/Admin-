using Admin.Services.Api;
using Admin.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISportPlusApiClient _apiClient;
        private readonly IConfiguration _configuration;

        public AccountController(ISportPlusApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = await _apiClient.LoginAsync(model);

            if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.AccessToken))
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View(model);
            }

            var roleId = loginResult.User?.RoleId ?? 0;

            HttpContext.Session.SetString("AccessToken", loginResult.AccessToken);
            HttpContext.Session.SetInt32("RoleId", roleId);
            HttpContext.Session.SetString("UserEmail", loginResult.User?.Email ?? "");
            HttpContext.Session.SetString("FullName", loginResult.User?.FullName ?? "");

            // Admin hoặc Staff -> vào trang quản trị
            if (roleId == 1 || roleId == 2)
            {
                return RedirectToAction("Index", "Admin");
            }

            // Customer -> chuyển sang trang người dùng
            if (roleId == 3)
            {
                var userPortalUrl = _configuration["AppRoutes:UserPortalUrl"];

                // Nếu bạn đã có web user riêng thì redirect sang đó
                if (!string.IsNullOrWhiteSpace(userPortalUrl))
                {
                    return Redirect(userPortalUrl);
                }

                // Nếu chưa có web user riêng thì hiển thị trang thông báo tạm
                return RedirectToAction(nameof(UserPortalRedirect));
            }

            HttpContext.Session.Clear();
            ModelState.AddModelError("", "Vai trò tài khoản không hợp lệ.");
            return View(model);
        }

        [HttpGet]
        public IActionResult UserPortalRedirect()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}