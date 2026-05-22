namespace Admin.ViewModels.Incidents
{
    public class IncidentListItemVm
    {
        public int IncidentId { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ReportedByName { get; set; }
    }

    public class IncidentDetailVm
    {
        public int IncidentId { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ReportedByName { get; set; }
        public string? HandledByName { get; set; }
        public string? HandleNote { get; set; }
        public DateTime? HandledAt { get; set; }
    }

    public class CreateIncidentVm
    {
        public int FieldId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class HandleIncidentVm
    {
        public int StatusId { get; set; }
        public string? HandleNote { get; set; }
    }
}