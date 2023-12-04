using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Sandbox.Api.Core.Addresses;
using Sandbox.Api.Core.Addresses.Commands.CreateAddress;
using Sandbox.Api.Core.Addresses.DTOs;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Repositories;
using static Sandbox.Api.Core.Tests.Addresses.Commands.CreateAddress.CreateAddressTestHelpers;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandlerTests
{
    private readonly Mock<IAddressRepository> _addressRepository;
    private readonly Mock<IValidator<CreateAddressCommand>> _validator;
    private readonly Mock<ValidationResult> _validationResult;
    private readonly IMapper _mapper;

    public CreateAddressCommandHandlerTests()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _validator = new Mock<IValidator<CreateAddressCommand>>();
        _validationResult = new Mock<ValidationResult>();

        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AddressProfile>());
        _mapper = new Mapper(mapperConfig);
    }

    [Fact]
    public void Handler_ShouldThrowValidationException_WhenValidationErrorOccurs()
    {
        var command = GetTestCommand();
        SetupValidationResult(false);

        var sut = CreateSut();
        var action = async () => await sut.Handle(command, default);

        action.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task Handler_ShouldReturnDto_WhenAddressCreated()
    {
        var command = GetTestCommand();
        var expectedAddress = _mapper.Map<Address>(command);
        var expectedDto = _mapper.Map<ReadAddressDto>(expectedAddress);
        
        SetupValidationResult();
        _addressRepository.Setup(repos => repos.CreateAsync(It.IsAny<Address>(), It.IsAny<CancellationToken>()));

        var sut = CreateSut();
        var result = await sut.Handle(command, default);

        result.Should().BeEquivalentTo(expectedDto);
    }

    private CreateAddressCommandHandler CreateSut() => new(_addressRepository.Object, _validator.Object, _mapper);

    private void SetupValidationResult(bool isValid = true)
    {
        _validationResult.SetupGet(vr => vr.IsValid).Returns(isValid);
        
        _validator.Setup(v => 
                v.ValidateAsync(It.IsAny<CreateAddressCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_validationResult.Object);
    }
}