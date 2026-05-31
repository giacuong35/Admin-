namespace Admin.ViewModels.Backups
{
    public class BackupPageVm
    {
        public List<BackupSnapshotVm> Snapshots { get; set; } = new();
        public BackupRestoreResultVm? LastRestoreResult { get; set; }
    }
}