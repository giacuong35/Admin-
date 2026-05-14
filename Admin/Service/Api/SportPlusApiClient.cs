using System.Net.Http.Headers;
using System.Net.Http.Json;
using Admin.ViewModels.Auth;
using Admin.ViewModels.Common;
using Admin.ViewModels.Users;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Services;

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
                email = model.Email,
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
    }
}