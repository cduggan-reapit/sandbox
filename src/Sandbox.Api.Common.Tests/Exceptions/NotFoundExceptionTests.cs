using FluentAssertions;
using Sandbox.Api.Common.Exceptions;

namespace Sandbox.Api.Common.Tests.Exceptions;

public class NotFoundExceptionTests
{
    /* public string ResourceType { get; set; }
    
    public Guid Id { get; set; }
    
    public NotFoundException(string type, Guid id) 
        : base ($"{type} not found matching Id \"{id}\"")
    {
        Id = id;
        ResourceType = type;
    }*/
    [Fact]
    public void Ctor_ShouldCreateException_FromProvidedParameters()
    {
        const string type = "entityType";
        var id = Guid.NewGuid();
        var expectedMessage = $"{type} not found matching Id \"{id}\"";

        var exception = new NotFoundException(type, id);

        exception.Message.Should().Be(expectedMessage);
        exception.ResourceType.Should().Be(type);
        exception.Id.Should().Be(id);
    }
}