using FluentValidation;
using Sandbox.Api.Core.Addresses.Enums;
using static Sandbox.Api.Core.Addresses.Commands.UpdateAddressById.UpdateAddressByIdCommandErrors;

namespace Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;

public class UpdateAddressByIdCommandValidator : AbstractValidator<UpdateAddressByIdCommand>
{
    public UpdateAddressByIdCommandValidator()
    {
        // Address Type must be valid address
        RuleFor(command => command.AddressType)
            .Must(addressType => AddressType.Values.Select(v => (string)v).Contains(addressType))
            .WithMessage(AddressTypeInvalid);

        // Number must be set and be less than or equal to 100 characters in length
        RuleFor(command => command.Number)
            .NotEmpty().WithMessage(NumberRequired)
            .MaximumLength(100).WithMessage(NumberExceedsMaxLength);
        
        // Street must be set and be less than or equal to 500 characters in length
        RuleFor(command => command.Street)
            .NotEmpty().WithMessage(StreetRequired)
            .MaximumLength(500).WithMessage(StreetExceedsMaxLength);
        
        // City must be set and be less than or equal to 100 characters in length
        RuleFor(command => command.City)
            .NotEmpty().WithMessage(CityRequired)
            .MaximumLength(100).WithMessage(CityExceedsMaxLength);
        
        // County must be less than or equal to 100 characters in length
        RuleFor(command => command.County)
            .MaximumLength(100).WithMessage(CountyExceedsMaxLength);
            
        // State must be less than or equal to 100 characters in length
        RuleFor(command => command.State)
            .MaximumLength(100).WithMessage(StateExceedsMaxLength);
        
        // Country must be set and be less than or equal to 100 characters in length
        RuleFor(command => command.Country)
            .NotEmpty().WithMessage(CountryRequired)
            .MaximumLength(100).WithMessage(CountryExceedsMaxLength);
        
        // Postcode must be set and be less than or equal to 50 characters in length
        RuleFor(command => command.PostCode)
            .NotEmpty().WithMessage(PostcodeRequired)
            .MaximumLength(50).WithMessage(PostcodeExceedsMaxLength);
    }
}