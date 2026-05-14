namespace Admin.ViewModels.Auth
{
    public class LoginResultVm
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public LoginUserVm? User { get; set; }
    }

    public class LoginUserVm
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public int RoleId { get; set; }
        public string? Status { get; set; }
        public int StatusId { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}