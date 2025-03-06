public class Role
{
    /// <summary>
    /// Pk, Unique Id, RoleGradeID
    /// </summary>
    public string RoleId { get; set; } = Guid.NewGuid().ToString();
    public long UserId { get; set; }

    /// <summary>
    /// Unique Id for every company
    /// </summary>
    public Guid BusinessId { get; set; } = Guid.NewGuid();
    public string RoleName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string GradeName { get; set; }
    public string GradeId { get; set; }
    public string BusinessUnitID { get; set; }
    public string ParentOrgUnit { get; set; }
    /// <summary>
    /// Active = 1, Inactive = 2, Deleted = 3
    /// </summary>
    public int Status { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}