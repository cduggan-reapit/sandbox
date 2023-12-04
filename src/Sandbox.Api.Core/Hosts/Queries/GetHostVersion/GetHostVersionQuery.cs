using MediatR;

namespace Sandbox.Api.Core.Hosts.Queries.GetHostVersion;

public record GetHostVersionQuery(bool ShouldPass = true) : IRequest<string>;