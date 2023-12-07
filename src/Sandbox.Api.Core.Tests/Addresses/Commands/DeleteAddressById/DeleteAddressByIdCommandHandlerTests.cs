using Sandbox.Api.Common.Exceptions;
using Sandbox.Api.Core.Addresses.Commands.DeleteAddressById;
using Sandbox.Api.Data.Entities;
using Sandbox.Api.Data.Helpers;
using Sandbox.Api.Data.Repositories;

namespace Sandbox.Api.Core.Tests.Addresses.Commands.DeleteAddressById;

public class DeleteAddressByIdCommandHandlerTests
{
    private readonly Mock<IAddressRepository> _addressRepository;
    
    public DeleteAddressByIdCommandHandlerTests()
    {
        _addressRepository = new Mock<IAddressRepository>();
    }

    [Fact]
    public async Task Handler_ShouldThrowNotFoundException_WhenIdNotFound()
    {
        var testId = Guid.NewGuid();
        var request = new DeleteAddressByIdCommand(testId, string.Empty);
        
        SetupAddressRepositoryGetById(testId, null);

        var sut = CreateSut();

        var action = () => sut.Handle(request, new CancellationToken());

        await action.Should().ThrowAsync<NotFoundException>().WithMessage(expectedWildcardPattern: "* not found matching Id \"*\"");
    }

    [Fact]
    public async Task Handler_ShouldThrowConflictException_WhenETagIncorrect()
    {
        var testId = Guid.NewGuid();
        var request = new DeleteAddressByIdCommand(testId, string.Empty);
        var address = new Address
        {
            Id = testId,
            Created = DateTimeOffset.UnixEpoch,
            Modified = DateTimeOffset.UnixEpoch
        };
        
        SetupAddressRepositoryGetById(testId, address);

        var sut = CreateSut();

        var action = () => sut.Handle(request, new CancellationToken());

        await action.Should().ThrowAsync<EntityConflictException>().WithMessage(expectedWildcardPattern: "ETag conflict. Expected * / Actual *");
    }

    [Fact]
    public async Task Handler_ShouldDeleteAddress_WhenCommandValid()
    {
        var testId = Guid.NewGuid();
        
        var address = new Address
        {
            Id = testId,
            Created = DateTimeOffset.UnixEpoch,
            Modified = DateTimeOffset.UnixEpoch
        };
        
        var request = new DeleteAddressByIdCommand(testId, address.GenerateEtagForEntity());
        
        SetupAddressRepositoryGetById(testId, address);

        _addressRepository.Setup(repository =>
                repository.DeleteAsync(It.Is<Address>(a => a.Id == address.Id), It.IsAny<CancellationToken>()))
            .Verifiable();
        
        var sut = CreateSut();

        await sut.Handle(request, new CancellationToken());
        
        _addressRepository.Verify(repository =>
                repository.DeleteAsync(It.Is<Address>(a => a.Id == address.Id), It.IsAny<CancellationToken>()), Times.Once);
    }

    private DeleteAddressByIdCommandHandler CreateSut() => new(_addressRepository.Object);
    
    private void SetupAddressRepositoryGetById(Guid id, Address? address)
        => _addressRepository.Setup(a =>
                a.GetByIdAsync(It.Is<Guid>(g => g == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(address);
}