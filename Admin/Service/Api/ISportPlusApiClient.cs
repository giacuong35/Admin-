using Admin.ViewModels.Auth;
using Admin.ViewModels.Users;
using Admin.ViewModels.Fields;
using Admin.ViewModels.Services;

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
    }
}