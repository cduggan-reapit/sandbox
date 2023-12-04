using AutoMapper;
using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;

public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, IEnumerable<ReadAddressDto>>
{
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepository;

    public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ReadAddressDto>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var addresses = await _addressRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ReadAddressDto>>(addresses);
    }
}