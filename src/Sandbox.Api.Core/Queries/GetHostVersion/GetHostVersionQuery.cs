using MediatR;

namespace Sandbox.Api.Core.Queries.GetHostVersion;

public record GetHostVersionQuery(bool ShouldPass = true) : IRequest<string>;