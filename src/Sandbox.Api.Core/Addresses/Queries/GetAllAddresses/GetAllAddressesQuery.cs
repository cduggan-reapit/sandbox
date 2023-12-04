using MediatR;
using Sandbox.Api.Core.Addresses.DTOs;

namespace Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;

public record GetAllAddressesQuery() : IRequest<IEnumerable<ReadAddressDto>>;