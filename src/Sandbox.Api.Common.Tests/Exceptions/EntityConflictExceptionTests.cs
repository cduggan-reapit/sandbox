using FluentAssertions;
using Sandbox.Api.Common.Exceptions;

namespace Sandbox.Api.Common.Tests.Exceptions;

public class EntityConflictExceptionTests
{
    [Fact]
    public void Ctor_ShouldCreateException_FromProvidedParameters()
    {
        const string expectedEtag = "expected";
        const string actualEtag = "actual";
        const string expectedMessage = $"ETag Conflict. Expected {expectedEtag} / Actual {actualEtag}";

        var exception = new EntityConflictException(expectedEtag, actualEtag);

        exception.Message.Should().Be(expectedMessage);
        exception.ExpectedETag.Should().Be(expectedEtag);
        exception.ActualETag.Should().Be(actualEtag);
    }
}