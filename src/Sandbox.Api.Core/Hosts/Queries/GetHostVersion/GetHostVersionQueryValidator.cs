using FluentValidation;

namespace Sandbox.Api.Core.Hosts.Queries.GetHostVersion;

public class GetHostVersionQueryValidator : AbstractValidator<GetHostVersionQuery>
{
    public GetHostVersionQueryValidator()
    {
        RuleFor(query => query)
            .Must(query => query.ShouldPass)
            .WithName(nameof(GetHostVersionQuery.ShouldPass))
            .WithMessage("Called with ShouldPass = false!");
    }
}