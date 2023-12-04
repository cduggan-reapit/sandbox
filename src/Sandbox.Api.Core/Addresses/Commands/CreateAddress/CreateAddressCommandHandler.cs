using AutoMapper;
using FluentValidation;
using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ReadAddressDto>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAddressCommand> _validator;

    public CreateAddressCommandHandler(
        IAddressRepository addressRepository, 
        IValidator<CreateAddressCommand> validator, 
        IMapper mapper)
    {
        _addressRepository = addressRepository;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task<ReadAddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var address = _mapper.Map<Address>(request);

        await _addressRepository.CreateAsync(address, cancellationToken);

        return _mapper.Map<ReadAddressDto>(address);
    }
}