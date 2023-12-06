using FluentValidation.Results;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Web.Errors;
using static Sandbox.Api.Web.Errors.ErrorModelMessages;

namespace Sandbox.Api.Tests.Web.Errors;

public class ErrorModelFactoryTests
{
    [Fact]
    public void GetErrorModel_ShouldCreateErrorModel_FromGivenValidationFailures()
    {
        var input = new[]
        {
            new ValidationFailure { PropertyName = "Property One", ErrorMessage = "Error One" },
            new ValidationFailure { PropertyName = "Property One", ErrorMessage = "Error Two" },
            new ValidationFailure { PropertyName = "Property Two", ErrorMessage = "Error Three" }
        };

        var expectedResult = new ErrorModel(
            Message: ValidationFailed,
            Errors: new Dictionary<string, string[]>
            {
                { "Property One", new [] { "Error One", "Error Two" } },
                { "Property Two", new [] { "Error Three" } }
            });

        var actual = input.GetErrorModel();

        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetErrorModel_ShouldCreateErrorModel_FromGivenException()
    {
        var exception = new InvalidDataException("Test exception");
        
        var expected = new ErrorModel(
            Message: ErrorModelMessages.InternalServerError,
            Errors: new Dictionary<string, string[]>
            {
                { "Message", new[] { exception.Message } },
                { "Type", new[] { exception.GetType().Name } }
            }); 
        
        var actual = exception.GetGenericErrorModel();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void GetErrorModel_ShouldCreateErrorModel_FromGivenNotFoundException()
    {
        var exception = new NotFoundException( typeof(BaseEntity), Guid.NewGuid());
        
        var expected = new ErrorModel(
            Message: ErrorModelMessages.NotFound,
            Errors: new Dictionary<string, string[]>
            {
                { "Type", new[] { exception.ResourceType.Name } },
                { "Id", new[] { exception.Id.ToString("D") } },
            }); 
        
        var actual = exception.GetErrorModel();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void GetErrorModel_ShouldCreateErrorModel_FromGivenEntityConflictException()
    {
        var exception = new EntityConflictException("Test exception");
        
        var expected = new ErrorModel(
            Message: ErrorModelMessages.Conflict,
            Errors: new Dictionary<string, string[]>
            {
                { "Message", new[] { exception.Message } },
            }); 
        
        var actual = exception.GetErrorModel();

        actual.Should().BeEquivalentTo(expected);
    }
}