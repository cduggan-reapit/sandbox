using MediatR;

namespace Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;

public record DeleteAddressByIdCommand(Guid Id, string? Etag) : IRequest;