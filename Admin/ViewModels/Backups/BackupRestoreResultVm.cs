namespace Admin.ViewModels.Backups
{
    public class BackupRestoreResultVm
    {
        public string? PreRestoreSnapshot { get; set; }
        public long ElapsedMs { get; set; }
        public Dictionary<string, int>? RestoredRows { get; set; }
        public int TotalRows { get; set; }
    }
}