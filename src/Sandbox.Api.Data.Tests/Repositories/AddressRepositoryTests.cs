using System.Linq.Expressions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Sandbox.Api.Common.Helpers;
using Sandbox.Api.Data.Context;
using Sandbox.Api.Data.Models.Entities;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Data.Tests.Repositories;

public class AddressRepositoryTests : IDisposable
{
    private bool _disposed;
    private readonly SqliteConnection _connection;
    private readonly SandboxDbContext _context;

    public AddressRepositoryTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<SandboxDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new SandboxDbContext(contextOptions);
        _context.Database.EnsureCreated();
    }
    
    // Sets Generated Properties
    
    [Fact]
    public async Task CreateAsync_SetsGeneratedProperties_WhenAddressCreated()
    {
        var timestamp = new DateTimeOffset(2018, 04, 01, 12, 30, 0, TimeSpan.Zero);
        DateTimeHelper.Set(timestamp);
        
        var testData = new Address
        {
            Number = "3206",
            Street = "353 N Desplaines St.",
            City = "Chicago",
            County = "Cook County",
            Country = "USA",
            PostCode = "60661"
        };
        
        var sut = CreateSut();
        await sut.CreateAsync(testData);

        
        testData.Id.Should().NotBe(Guid.Empty);
        testData.Created.Should().Be(timestamp);
        testData.Modified.Should().Be(timestamp);
    }
    
    [Fact]
    public async Task CreateAsync_UpdatesGeneratedProperties_WhenAddressModified()
    {
        var createTimestamp = new DateTimeOffset(2018, 04, 01, 12, 30, 0, TimeSpan.Zero);
        var modifyTimestamp = new DateTimeOffset(2020, 12, 31, 5, 15, 45, TimeSpan.Zero);
        DateTimeHelper.Set(createTimestamp);
        
        var testData = new Address
        {
            Number = "3206",
            Street = "353 N Desplaines St.",
            City = "Chicago",
            County = "Cook County",
            Country = "USA",
            PostCode = "60661"
        };
        
        var sut = CreateSut();
        await sut.CreateAsync(testData);
        
        DateTimeHelper.Set(modifyTimestamp);
        testData.Number = "Modified";
        await sut.UpdateAsync(testData);

        var result = await sut.GetByIdAsync(testData.Id);

        result.Should().NotBeNull();
        result!.Id.Should().NotBe(Guid.Empty);
        result.Created.Should().Be(createTimestamp);
        result.Modified.Should().Be(modifyTimestamp);
    }
    
    // Create Addresses

    [Fact]
    public async Task CreateAsync_CreatesAddress_WhenAddressProvided()
    {
        var timestamp = new DateTimeOffset(2018, 04, 01, 12, 30, 0, TimeSpan.Zero);
        DateTimeHelper.Set(timestamp);
        
        var testData = new Address
        {
            Number = "3206",
            Street = "353 N Desplaines St.",
            City = "Chicago",
            County = "Cook County",
            Country = "USA",
            PostCode = "60661"
        };
        
        var sut = CreateSut();
        await sut.CreateAsync(testData);

        var actual = await _context.Addresses.FirstAsync();
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(testData);
    }
    
    [Fact]
    public async Task CreateAsync_CreatesAddress_WhenMultipleAddressesProvided()
    {
        var testData = GenerateTestData(3);
        
        var sut = CreateSut();
        await sut.CreateRangeAsync(testData);

        var actual = await _context.Addresses.ToListAsync();
        actual.Should().HaveCount(3);
    }
    
    // Read Addresses

    [Fact]
    public async Task GetAsync_ReturnsEmptyCollection_WhenDatabaseEmpty()
    {
        var sut = CreateSut();
        var result = await sut.GetAsync();
        
        result.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task GetAsync_ReturnsAllAddresses_WhenAddressesInDatabase()
    {
        var testData = GenerateTestData(5).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();
        
        var sut = CreateSut();
        var result = await sut.GetAsync() as List<Address>;
        
        result.Should().NotBeNull();
        result.Should().HaveCount(5);
        result.Should().BeEquivalentTo(testData);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenIdNotFound()
    {
        var sut = CreateSut();
        var result = await sut.GetByIdAsync(Guid.Empty);
        
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsAddress_WhenIdExists()
    {
        var testData = GenerateTestData(1).First();
        
        await _context.Addresses.AddAsync(testData);
        await _context.SaveChangesAsync();
        
        var sut = CreateSut();
        var result = await sut.GetByIdAsync(testData.Id);
        
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(testData);
    }
    
    [Fact]
    public async Task GetFilteredAsync_ReturnsEmptyCollection_WhenFilterHasNoResults()
    {
        var testData = GenerateTestData(3).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();
        
        var sut = CreateSut();
        var result = await sut.GetFilteredAsync(address => address.Number == "000") as List<Address>;
        
        result.Should().HaveCount(0);
    }
    
    [Fact]
    public async Task GetFilteredAsync_ReturnsAllAddresses_WhenMatchingFilter()
    {
        var testData = GenerateTestData(3).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();

        var filterCollection = new[] { "001", "003" };
        Expression<Func<Address, bool>> filter = address => filterCollection.Contains(address.Number);
        
        var sut = CreateSut();
        var result = await sut.GetFilteredAsync(filter) as List<Address>;
        
        result.Should().HaveCount(2);
    }
    
    // Update Addresses
    
    [Fact]
    public async Task UpdateAsync_UpdatesAddress_WhenModifiedEntityProvided()
    {
        var timestamp = new DateTimeOffset(2018, 04, 01, 12, 30, 0, TimeSpan.Zero);
        DateTimeHelper.Set(timestamp);
        
        var testData = new Address { Number = "3206", Street = "353 N St.", City = "Chicago", County = "Cook", Country = "USA", PostCode = "60661" };
        
        await _context.Addresses.AddAsync(testData);
        await _context.SaveChangesAsync();

        const string expectedNumber = "3201";
        testData.Number = expectedNumber;
        
        var sut = CreateSut();
        await sut.UpdateAsync(testData);

        var actual = await _context.Addresses.FirstAsync();
        actual.Number.Should().Be(expectedNumber);
    }
    
    [Fact]
    public async Task UpdateRangeAsync_UpdatesAddress_WhenMultipleModifiedAddressesProvided()
    {
        var testData = GenerateTestData(2).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();
        
        var expectedStreets = new Dictionary<Guid, string>
        {
            { testData.ElementAt(0).Id, "Fake Street" }, 
            { testData.ElementAt(1).Id, "Letsby Avenue" }
        };
        
        // Update the entities
        foreach (var address in testData)
        {
            address.Street = expectedStreets[address.Id];
        }
        
        var sut = CreateSut();
        await sut.UpdateRangeAsync(testData);

        var actual = await _context.Addresses.OrderBy(a => a.Number).ToListAsync();
        actual.Select(a => a.Street).Should().Equal(expectedStreets.Select(s => s.Value));
    }
    
    // Delete Addresses
    
    [Fact]
    public async Task DeleteAsync_DeletesAddress_WhenIdProvided()
    {
        var testData = GenerateTestData(5).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();

        var deleteId = testData.ElementAt(3).Id;
        
        var sut = CreateSut();
        await sut.DeleteAsync(deleteId);

        var deleted = await _context.Addresses.FindAsync(deleteId);
        deleted.Should().BeNull();

        var retained = await _context.Addresses.ToListAsync();
        retained.Should().HaveCount(4);
    }
    
    [Fact]
    public async Task DeleteRangeAsync_DeletesAddresses_WhenMultipleIdsProvided()
    {
        var testData = GenerateTestData(5).ToList();
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();
        
        var toDelete = new[]
        {
            testData.ElementAt(1).Id, 
            testData.ElementAt(3).Id
        };
        
        var sut = CreateSut();
        var result = await sut.DeleteRangeAsync(toDelete);

        result.Should().BeTrue();

        var deleted = await _context.Addresses.Where(a => toDelete.Contains(a.Id)).ToListAsync();
        deleted.Should().HaveCount(0);

        var retained = await _context.Addresses.ToListAsync();
        retained.Should().HaveCount(3);
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldNotDeleteAddresses_WhenAnInvalidIdProvidedInCollection()
    {
        var testData = GenerateTestData(5).ToList();
        var toDelete = new[] { GenerateGuid(2), GenerateGuid(6) };
        
        await _context.Addresses.AddRangeAsync(testData);
        await _context.SaveChangesAsync();
        
        var sut = CreateSut();
        var result = await sut.DeleteRangeAsync(toDelete);
        
        result.Should().BeFalse();
        
        var retained = await _context.Addresses.ToListAsync();
        retained.Should().HaveCount(5);
    }

    // Private members
    
    private AddressRepository CreateSut() => new (_context);

    private static IEnumerable<Address> GenerateTestData(int count, int offset = 1)
        => Enumerable.Range(offset, count)
            .Select(i => new Address
            {
                Id = GenerateGuid(i),
                Number = $"{i:D3}",
                Street = $"{i:D3} Street",
                City = $"{i:D3} City",
                Country = $"{i:D3} Country",
                PostCode = $"{i:D9}"
            });

    private static Guid GenerateGuid(int guidSeed) => new ($"{guidSeed:D32}");
    
    // Disposal
    
    ~AddressRepositoryTests()
    {
        Dispose(false);        
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) 
            return;
        
        if (disposing)
        {
            _context.Dispose();
            _connection.Dispose();
        }
            
        _disposed = true;
    }
}