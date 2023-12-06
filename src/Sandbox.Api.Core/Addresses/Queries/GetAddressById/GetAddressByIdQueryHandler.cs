using AutoMapper;
using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Queries.GetAddressById;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, ReadAddressDto?>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }
    
    public async Task<ReadAddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

        return address == null ? null 
            : _mapper.Map<ReadAddressDto>(address);
    }
}