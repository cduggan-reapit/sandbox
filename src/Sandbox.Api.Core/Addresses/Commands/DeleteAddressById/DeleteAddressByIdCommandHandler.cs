using AutoMapper;
using MediatR;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;

public class DeleteAddressByIdCommandHandler : IRequestHandler<DeleteAddressByIdCommand>
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public DeleteAddressByIdCommandHandler(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteAddressByIdCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

        if (address == null)
            throw new NotFoundException(typeof(Address), request.Id);

        // TODO: Decide what to put in this error
        if (request.Etag != address.GenerateEtagForEntity())
            throw new EntityConflictException("");
        
        await _addressRepository.DeleteAsync(address, cancellationToken);
    }
}