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

        const string expectedEtag = "e3018d8d6a53c4957a60267038ab053f";
        var firstEtag = entity.GenerateEtagForEntity();
        var secondEtag = entity.GenerateEtagForEntity();
        
        firstEtag.Should().Be(expectedEtag);
        firstEtag.Should().Be(secondEtag);
    }
    
    [Fact]
    public void GenerateEtagForEntity_ShouldGenerateNewEtag_ForDifferentEntityTypes()
    {
        // Note: this should never happen in reality, but checks that entity type is used in generating the etag
        const string guidString = "00000000-0000-0000-0000-000000000001";
        var addressEntity = new Address { Id = new Guid(guidString), Created = DateTimeOffset.UnixEpoch, Modified = DateTimeOffset.UnixEpoch };
        var baseEntity = addressEntity as BaseEntity;
        
        var firstEtag = addressEntity.GenerateEtagForEntity();
        var secondEtag = baseEntity.GenerateEtagForEntity();
        
        firstEtag.Should().NotBe(secondEtag);
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

    [Fact]
    public void IsETagValid_ReturnsTrue_WhenETagsIdentical()
    {
        var guid = new Guid("00000000-0000-0000-0000-000000000002");
        var timestamp = DateTimeOffset.UnixEpoch.AddYears(1);
        var entity = new Address { Id = guid, Created = timestamp, Modified = timestamp } as BaseEntity;

        var expectedETag = entity.GenerateEtagForEntity();

        var result = entity.IsETagValid(expectedETag);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsETagValid_ReturnsTrue_WhenProvidedETagInQuotes()
    {
        var guid = new Guid("00000000-0000-0000-0000-000000000002");
        var timestamp = DateTimeOffset.UnixEpoch.AddYears(1);
        var entity = new Address { Id = guid, Created = timestamp, Modified = timestamp } as BaseEntity;

        var expectedETag = string.Concat('"', entity.GenerateEtagForEntity(), '"');

        var result = entity.IsETagValid(expectedETag);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsETagValid_ReturnsTrue_WhenOnlyCasingDifferent()
    {
        var guid = new Guid("00000000-0000-0000-0000-000000000002");
        var timestamp = DateTimeOffset.UnixEpoch.AddYears(1);
        var entity = new Address { Id = guid, Created = timestamp, Modified = timestamp } as BaseEntity;

        // ETags are lowercase by default, cast to uppercase for test
        var expectedETag = entity.GenerateEtagForEntity().ToUpper();

        var result = entity.IsETagValid(expectedETag);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsETagValid_ReturnsFalse_WhenETagIsDifferent()
    {
        var guid = new Guid("00000000-0000-0000-0000-000000000002");
        var timestamp = DateTimeOffset.UnixEpoch.AddYears(1);
        var entity = new Address { Id = guid, Created = timestamp, Modified = timestamp } as BaseEntity;
        
        var expectedETag = entity.GenerateEtagForEntity();

        entity.Modified = timestamp.AddYears(1);

        var result = entity.IsETagValid(expectedETag);
        result.Should().BeFalse();
    }
}