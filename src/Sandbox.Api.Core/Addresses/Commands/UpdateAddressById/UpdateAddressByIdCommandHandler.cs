using AutoMapper;
using FluentValidation;
using MediatR;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Enums;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;

public class UpdateAddressByIdCommandHandler : IRequestHandler<UpdateAddressByIdCommand, ReadAddressDto>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IValidator<UpdateAddressByIdCommand> _validator;
    private readonly IMapper _mapper;

    public UpdateAddressByIdCommandHandler(
        IAddressRepository addressRepository, 
        IValidator<UpdateAddressByIdCommand> validator, 
        IMapper mapper)
    {
        _addressRepository = addressRepository;
        _validator = validator;
        _mapper = mapper;
    }
    
    /// <inheritdoc/>
    public async Task<ReadAddressDto> Handle(UpdateAddressByIdCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Address), request.Id);

        if (!address.IsETagValid(request.ETag))
            throw new EntityConflictException(address.GenerateEtagForEntity(), request.ETag);
        
        // Update Address from UpdateAddressByIdCommand
        address.AddressType = (AddressType)request.AddressType;
        address.Number = request.Number;
        address.Street = request.Street;
        address.City = request.City;
        address.County = request.County;
        address.State = request.State;
        address.Country = request.Country;
        address.PostCode = request.PostCode;

        await _addressRepository.UpdateAsync(address, cancellationToken);

        return _mapper.Map<ReadAddressDto>(address);
    }
}