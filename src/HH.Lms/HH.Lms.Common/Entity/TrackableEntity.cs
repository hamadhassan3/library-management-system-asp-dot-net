using System.ComponentModel.DataAnnotations;

namespace HH.Lms.Common.Entity;

public abstract class TrackableEntity : IBaseEntity
{
    [Display(Name = "Revisions", AutoGenerateField = false)]
    [ScaffoldColumn(false)]
    public int Version { get; set; } = 1;

    [Display(Name = "Created", AutoGenerateField = false)]
    [ScaffoldColumn(false)]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Display(Name = "Updated", AutoGenerateField = false)]
    [ScaffoldColumn(false)]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public void Touch()
    {
        Version++;
        Updated = DateTime.UtcNow;
    }
}
