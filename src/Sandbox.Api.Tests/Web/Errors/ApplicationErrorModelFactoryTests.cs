using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Web.Errors;
using static Sandbox.Api.Web.Errors.ApplicationErrorModelMessages;

namespace Sandbox.Api.Tests.Web.Errors;

public class ApplicationErrorModelFactoryTests
{
    // NotFound

    [Fact]
    public void GetNotFoundErrorModel_ShouldReturnErrorModel_WhenNotFoundExceptionProvided()
    {
        var exception = new NotFoundException(nameof(BaseEntity), id: Guid.NewGuid());

        var expected = new ApplicationErrorModel(NotFound, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Type", exception.ResourceType },
            { "Id", exception.Id.ToString() }
        });

        var actual = exception.GetNotFoundErrorModel();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void GetNotFoundErrorModel_ShouldReturnErrorModel_WhenNotFoundParametersProvided()
    {
        var exception = new NotFoundException(nameof(BaseEntity), id: Guid.NewGuid());

        var expected = new ApplicationErrorModel(NotFound, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Type", exception.ResourceType },
            { "Id", exception.Id.ToString() }
        });

        var actual = ApplicationErrorModelFactory.GetNotFoundErrorModel(
            exception.Message, 
            exception.ResourceType, 
            exception.Id.ToString()
        );

        actual.Should().BeEquivalentTo(expected);
    }

    // Conflict
    
    [Fact]
    public void GetConflictErrorModel_ShouldReturnErrorModel_WhenConflictExceptionProvided()
    {
        var exception = new EntityConflictException("expected","actual");

        var expected = new ApplicationErrorModel(Conflict, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Expected", exception.ExpectedETag },
            { "Actual", exception.ActualETag }
        });

        var actual = exception.GetConflictErrorModel();

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void GetConflictErrorModel_ShouldReturnErrorModel_WhenConflictParametersProvided()
    {
        var exception = new EntityConflictException("expected","actual");

        var expected = new ApplicationErrorModel(Conflict, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Expected", exception.ExpectedETag },
            { "Actual", exception.ActualETag }
        });
        
        var actual = ApplicationErrorModelFactory.GetConflictErrorModel(
            exception.Message, 
            exception.ExpectedETag, 
            exception.ActualETag
        );

        actual.Should().BeEquivalentTo(expected);
    }
    
    // Exception
    
    [Fact]
    public void GetExceptionErrorModel_ShouldReturnErrorModel_WhenExceptionProvided()
    {
        var exception = new ApplicationException("test message");

        var expected = new ApplicationErrorModel(InternalError, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Type", exception.GetType().Name },
            { "HResult", exception.HResult.ToString() }
        });

        var actual = exception.GetExceptionErrorModel();
        
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void GetExceptionErrorModel_ShouldReturnErrorModel_WhenExceptionParametersProvided()
    {
        var exception = new ApplicationException("test message");

        var expected = new ApplicationErrorModel(InternalError, new Dictionary<string, string>
        {
            { "Description", exception.Message },
            { "Type", exception.GetType().Name },
            { "HResult", exception.HResult.ToString() }
        });
        
        var actual = ApplicationErrorModelFactory.GetExceptionErrorModel(
            exception.Message, 
            exception.GetType().Name, 
            exception.HResult.ToString()
        );

        actual.Should().BeEquivalentTo(expected);
    }
}