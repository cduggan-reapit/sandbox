using AutoMapper;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Core.Addresses.Queries.GetAddressById;
using Sandbox.Api.Core.Tests.TestHelpers;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Tests.Addresses.Queries.GetAddressById;

public class GetAddressByIdQueryHandlerTests
{
    private readonly Mock<IAddressRepository> _addressRepository;
    private readonly IMapper _mapper;

    public GetAddressByIdQueryHandlerTests()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _mapper = AutomapperHelper.GetCoreMapper();
    }

    [Fact]
    public async Task Handler_ShouldThrowNotFoundException_WhenIdNotFound()
    {
        var testId = Guid.NewGuid();
        var request = new GetAddressByIdQuery(testId);
        SetupAddressRepositoryGetById(testId, null);

        var sut = CreateSut();
        var action = () => sut.Handle(request, default);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handler_ShouldReturnMappedDto_WhenAddressFound()
    {
        var testId = Guid.NewGuid();
        var request = new GetAddressByIdQuery(testId);
        
        var address = new Address
        {
            Id = testId,
            Created = DateTimeOffset.MinValue,
            Modified = DateTimeOffset.UnixEpoch,
            AddressType = 1,
            City = "city",
            Country = "country",
            County = "county",
            Number = "number",
            PostCode = "postcode",
            State = "state"
        };
        
        var addressDto = _mapper.Map<ReadAddressDto>(address);
        
        SetupAddressRepositoryGetById(testId, address);

        var sut = CreateSut();
        var result = await sut.Handle(request, default);

        result.Should().BeEquivalentTo(addressDto);
    }

    private GetAddressByIdQueryHandler CreateSut() => new(_addressRepository.Object, _mapper);
    
    private void SetupAddressRepositoryGetById(Guid id, Address? address)
        => _addressRepository.Setup(a =>
                a.GetByIdAsync(It.Is<Guid>(g => g == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(address);
}