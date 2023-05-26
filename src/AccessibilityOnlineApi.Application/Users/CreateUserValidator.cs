using AccessibilityOnlineApi.Application.Dtos;
using FluentValidation;

namespace AccessibilityOnlineApi.Application.Users;

public class CreateUserValidator : AbstractValidator<CreateUserDTO>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.AgeRange).MaximumLength(8);
        RuleFor(x => x.AgeRange).NotNull();
        RuleFor(x => x.AgeRange).NotEmpty();
        RuleFor(x => x.AssistiveSoftware).MaximumLength(512);
        RuleFor(x => x.AssistiveSoftware).NotNull();
        RuleFor(x => x.AssistiveSoftware).NotEmpty();
        RuleFor(x => x.CanPublish).NotNull();
        RuleFor(x => x.Confidence).MaximumLength(128);
        RuleFor(x => x.Confidence).NotNull();
        RuleFor(x => x.Confidence).NotEmpty();
        RuleFor(x => x.Country).MaximumLength(64);
        RuleFor(x => x.Country).NotNull();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.Devices).MaximumLength(512);
        RuleFor(x => x.Devices).NotNull();
        RuleFor(x => x.Devices).NotEmpty();
        RuleFor(x => x.Email).MaximumLength(256);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.FirstName).MaximumLength(64);
        RuleFor(x => x.FirstName).NotNull();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.FurtherAs).MaximumLength(512);
        RuleFor(x => x.FurtherDevice).MaximumLength(512);
        RuleFor(x => x.FurtherOs).MaximumLength(512);
        RuleFor(x => x.IsTester).NotNull();
        RuleFor(x => x.LastName).MaximumLength(64);
        RuleFor(x => x.LastName).NotNull();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.NextProjectPreference).MaximumLength(32);
        RuleFor(x => x.NextProjectPreference).NotNull();
        RuleFor(x => x.NextProjectPreference).NotEmpty();
        RuleFor(x => x.OperatingSystems).MaximumLength(512);
        RuleFor(x => x.OperatingSystems).NotNull();
        RuleFor(x => x.OperatingSystems).NotEmpty();
        RuleFor(x => x.Sight).MaximumLength(32);
        RuleFor(x => x.Sight).NotNull();
        RuleFor(x => x.Sight).NotEmpty();
        RuleFor(x => x.Statement).MaximumLength(512);
        RuleFor(x => x.Website).MaximumLength(512);
    }
}