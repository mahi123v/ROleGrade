public class Grade
{
    /// <summary>
    /// Pk, Unique Id, GradeID
    /// </summary>
    public string GradeID { get; set; } = Guid.NewGuid().ToString();

    public long UserId { get; set; }

    /// <summary>
    /// Unique Id for every company
    /// </summary>
    public Guid BusinessId { get; set; } = Guid.NewGuid();

    public string GradeName { get; set; }
    public string GradeLevel { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Active = 1, Inactive = 2, Deleted = 3
    /// </summary>
    public int Status { get; set; }

    public string Description { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
