using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;

namespace Sandbox.Api.Data.Tests.Helpers;

public class EntityTagHelperTests
{
    [Fact]
    public void GenerateEtagForEntity_ShouldGenerateEtagConsistently_ForSameEntity()
    {
        const string guidString = "00000000-0000-0000-0000-000000000001";
        var entity = new Address { Id = new Guid(guidString), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.UnixEpoch } as BaseEntity;

        const string expectedEtag = "\"b0e4c11033528bd065ace92499fc523a\"";
        var firstEtag = entity.GenerateEtagForEntity();
        var secondEtag = entity.GenerateEtagForEntity();
        
        firstEtag.Should().Be(expectedEtag);
        firstEtag.Should().Be(secondEtag);
    }
    
    [Fact]
    public void GenerateEtagForEntity_ShouldGenerateNewEtag_WhenModifiedDateChanged()
    {
        var initial = DateTimeOffset.UnixEpoch;
        var modified = DateTimeOffset.Now;
        
        var entity = new Address { Id = Guid.NewGuid(), Created = initial, Modified = initial } as BaseEntity;
        var initialEtag = entity.GenerateEtagForEntity();

        entity.Modified = modified;
        var modifiedEtag = entity.GenerateEtagForEntity();

        initialEtag.Should().NotBeEquivalentTo(modifiedEtag);
    }
    
    [Fact]
    public void GenerateEtagForEntity_ShouldGenerateDifferentEtag_ForDifferentEntities()
    {
        var firstEntity = new Address { Id = new Guid("00000000-0000-0000-0000-000000000001"), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.UnixEpoch } as BaseEntity;
        var secondEntity = new Address { Id = new Guid("00000000-0000-0000-0000-000000000002"), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.UnixEpoch } as BaseEntity;

        var firstEtag = firstEntity.GenerateEtagForEntity();
        var secondEtag = secondEntity.GenerateEtagForEntity();
        
        firstEtag.Should().NotBeEquivalentTo(secondEtag);
    }
}