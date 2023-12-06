using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;

namespace Sandbox.Api.Core.Addresses.Queries.GetAddressById;

public record GetAddressByIdQuery(Guid Id) : IRequest<ReadAddressDto>;