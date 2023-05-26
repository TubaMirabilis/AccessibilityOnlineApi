using System.ComponentModel.DataAnnotations;

namespace AccessibilityOnlineApi.Application.Dtos;

public record CreateUserDTO
{
    [StringLength(64)]
    public required string FirstName { get; init; }
    [StringLength(64)]
    public required string LastName { get; init; }
    [StringLength(256)]
    public required string Email { get; init; }
    [StringLength(8)]
    public required string AgeRange { get; init; }
    [StringLength(64)]
    public required string Country { get; init; }
    [StringLength(32)]
    public required string Sight { get; init; }
    [StringLength(128)]
    public required string Confidence { get; init; }
    [StringLength(512)]
    public required string Devices { get; init; }
    [StringLength(512)]
    public string? FurtherDevice { get; init; }
    [StringLength(512)]
    public required string OperatingSystems { get; init; }
    [StringLength(512)]
    public string? FurtherOs { get; init; }
    [StringLength(512)]
    public required string AssistiveSoftware { get; init; }
    [StringLength(512)]
    public string? FurtherAs { get; init; }
    [StringLength(32)]
    public required string NextProjectPreference { get; init; }
    public required bool IsTester { get; init; }
    [StringLength(512)]
    public string? Website { get; init; }
    [StringLength(512)]
    public string? Statement { get; init; }
    public required bool CanPublish { get; init; }
}