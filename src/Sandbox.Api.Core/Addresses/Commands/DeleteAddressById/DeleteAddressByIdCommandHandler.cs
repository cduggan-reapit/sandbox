using MediatR;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;

public class DeleteAddressByIdCommandHandler : IRequestHandler<DeleteAddressByIdCommand>
{
    private readonly IAddressRepository _addressRepository;

    public DeleteAddressByIdCommandHandler(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(DeleteAddressByIdCommand request, CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetByIdAsync(request.Id, cancellationToken);

        if (address == null)
            throw new NotFoundException(nameof(Address), request.Id);
        
        if (!address.IsETagValid(request.Etag ?? string.Empty))
            throw new EntityConflictException(address.GenerateEtagForEntity(), request.Etag ?? string.Empty);
        
        await _addressRepository.DeleteAsync(address, cancellationToken);
    }
}