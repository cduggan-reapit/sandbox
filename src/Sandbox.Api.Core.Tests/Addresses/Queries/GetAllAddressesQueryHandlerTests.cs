using AutoMapper;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAllAddresses;
using Sandbox.Api.Core.Tests.TestHelpers;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Tests.Addresses.Queries;

public class GetAllAddressesQueryHandlerTests
{
    private readonly Mock<IAddressRepository> _addressRepository;
    private readonly IMapper _mapper;

    public GetAllAddressesQueryHandlerTests()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _mapper = AutomapperHelper.GetCoreMapper();
    }

    [Fact]
    public async Task Handle_ReturnsEmptyCollection_WhenNoAddressesInRepository()
    {
        _addressRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<Address>());

        var sut = CreateSut();

        var result = await sut.Handle(new GetAllAddressesQuery(), default);
        
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Handle_ReturnsCollection_WhenAddressesInRepository()
    {
        var addresses = new[]
        {
            new Address
            {
                Id = Guid.NewGuid(), AddressType = 0, Number = "0-street", Street = "0-street", City = "0-city",
                Country = "0-country", PostCode = "0-postcode", Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now
            },
            new Address
            {
                Id = Guid.NewGuid(), AddressType = 1, Number = "1-street", Street = "1-street", City = "1-city",
                Country = "1-country", PostCode = "1-postcode", Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now
            },
            new Address
            {
                Id = Guid.NewGuid(), AddressType = 2, Number = "2-street", Street = "2-street", City = "2-city",
                Country = "2-country", PostCode = "2-postcode", Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now
            }
        };

        var expected = _mapper.Map<IEnumerable<ReadAddressDto>>(addresses);    
        
        _addressRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(addresses);

        var sut = CreateSut();

        var result = await sut.Handle(new GetAllAddressesQuery(), default);

        var resultList = result.ToList();
        resultList.Should().HaveCount(3);
        resultList.Should().BeEquivalentTo(expected);
    }

    private GetAllAddressesQueryHandler CreateSut() => new(_addressRepository.Object, _mapper);
}