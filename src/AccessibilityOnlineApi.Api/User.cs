using System.ComponentModel.DataAnnotations;

public class User
{
    public Guid Id { get; set; }
    [StringLength(64)]
    public required string FirstName { get; set; }
    [StringLength(64)]
    public required string LastName { get; set; }
    [StringLength(256)]
    public required string Email { get; set; }
    [StringLength(8)]
    public required string AgeRange { get; set; }
    [StringLength(64)]
    public required string Country { get; set; }
    [StringLength(32)]
    public required string Sight { get; set; }
    [StringLength(128)]
    public required string Confidence { get; set; }
    [StringLength(512)]
    public required string Devices { get; set; }
    [StringLength(512)]
    public string? FurtherDevice { get; set; }
    [StringLength(512)]
    public required string OperatingSystems { get; set; }
    [StringLength(512)]
    public string? FurtherOs { get; set; }
    [StringLength(512)]
    public required string AssistiveSoftware { get; set; }
    [StringLength(512)]
    public string? FurtherAs { get; set; }
    [StringLength(32)]
    public required string NextProjectPreference { get; set; }
    public required bool IsTester { get; set; }
    [StringLength(512)]
    public string? Website { get; set; }
    [StringLength(512)]
    public string? Statement { get; set; }
    public required bool CanPublish { get; set; }
    public DateTime CreatedAt { get; set; }
}