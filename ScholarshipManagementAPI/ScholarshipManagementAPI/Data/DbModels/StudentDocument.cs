using System;
using System.Collections.Generic;

namespace ScholarshipManagementAPI.Data.DbModels;

public partial class StudentDocument
{
    public long DocumentId { get; set; }

    public long StudentId { get; set; }

    public string? DocType { get; set; }

    public string? DocName { get; set; }

    public string? FileUrlName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual StudentDatum Student { get; set; } = null!;
}
