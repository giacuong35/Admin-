namespace Admin.ViewModels.Backups
{
    public class BackupSnapshotVm
    {
        public string FileName { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        public string SizeLabel { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedAtLabel { get; set; } = string.Empty;
    }
}