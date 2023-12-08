using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses;
using Sandbox.Api.Core.Addresses.Commands.UpdateAddressById;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;
using Sandbox.Api.Data.Repositories;
using static Sandbox.Api.Core.Tests.Addresses.Commands.UpdateAddressById.UpdateAddressByIdTestHelpers;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.UpdateAddressById;

public class UpdateAddressByIdCommandHandlerTests
{
    private readonly Mock<IAddressRepository> _addressRepository;
    private readonly Mock<IValidator<UpdateAddressByIdCommand>> _validator;
    private readonly Mock<ValidationResult> _validationResult;
    private readonly IMapper _mapper;

    public UpdateAddressByIdCommandHandlerTests()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _validator = new Mock<IValidator<UpdateAddressByIdCommand>>();
        _validationResult = new Mock<ValidationResult>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AddressProfile>());
        _mapper = new Mapper(mapperConfig);
    }
    
    [Fact]
    public async Task Handler_ShouldThrowValidationException_WhenValidationFails()
    {
        var model = GetTestCommand();
        SetupValidation(false);

        var sut = CreateSut();

        var action = () => sut.Handle(model, default);
        await action.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task Handler_ShouldThrowNotFoundException_WhenAddressNotFound()
    {
        var model = GetTestCommand();
        SetupValidation(true);
        SetupAddressRepositoryGetById(model.Id, null);

        var sut = CreateSut();

        var action = () => sut.Handle(model, default);
        await action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Handler_ShouldEntityConflictException_WhenETagDoesntMatch()
    {
        var guid = new Guid("00000000-0000-0000-0000-111111111111");
        const string etag = "this-is-the-wrong-etag";
        var model = GetTestCommand(id: guid, etag: etag);
        var address = new Address { Id = guid, Modified = DateTimeOffset.UnixEpoch };
        
        SetupValidation(true);
        SetupAddressRepositoryGetById(model.Id, address);

        var sut = CreateSut();

        var action = () => sut.Handle(model, default);
        await action.Should().ThrowAsync<EntityConflictException>();
    }
    
    [Fact]
    public async Task Handler_ShouldReturnUpdatedDto_WhenCommandValid()
    {
        var guid = new Guid("00000000-0000-0000-0000-111111111111");
        
        var address = new Address { Id = guid, Modified = DateTimeOffset.UnixEpoch, AddressType = 2};
        
        var model = GetTestCommand(
            id: guid, 
            etag: address.GenerateEtagForEntity(),
            number: "new-number",
            street: "new-street",
            city: "new-city",
            county: "new-county",
            state: "new-state",
            country: "new-country",
            postCode: "new-postcode",
            addressType: "residential"
        );
        
        SetupValidation(true);
        SetupAddressRepositoryGetById(model.Id, address);

        var sut = CreateSut();
        var result = await sut.Handle(model, default);

        result.Number.Should().Be(model.Number);
        result.Street.Should().Be(model.Street);
        result.City.Should().Be(model.City);
        result.County.Should().Be(model.County);
        result.State.Should().Be(model.State);
        result.Country.Should().Be(model.Country);
        result.PostCode.Should().Be(model.PostCode);
        result.AddressType.Should().Be(model.AddressType);
    }
    
    private UpdateAddressByIdCommandHandler CreateSut() 
        => new(_addressRepository.Object, _validator.Object, _mapper);

    private void SetupAddressRepositoryGetById(Guid id, Address? address)
        => _addressRepository.Setup(ar =>
                ar.GetByIdAsync(It.Is<Guid>(guid => guid.Equals(id)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(address);

    private void SetupValidation(bool success)
    {
        _validationResult.SetupGet(vr => vr.IsValid).Returns(success);
        _validator.Setup(v => v.ValidateAsync(It.IsAny<UpdateAddressByIdCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_validationResult.Object);
    }
}