using System.Reflection;
using FluentValidation;
using MediatR;

namespace Sandbox.Api.Core.Queries.GetHostVersion;

public class GetHostVersionQueryHandler : IRequestHandler<GetHostVersionQuery, string>
{
    private readonly IValidator<GetHostVersionQuery> _validator;
    
    public GetHostVersionQueryHandler(IValidator<GetHostVersionQuery> validator)
    {
        _validator = validator;
    }
    
    public async Task<string> Handle(GetHostVersionQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;
    }
}