namespace Admin.ViewModels.Users
{
    public class UserListItemVm
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public string? Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}