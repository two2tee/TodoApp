using System;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.DomainObjects.Repositories;
using Xunit;

namespace TodoTests;

public class MemoryRepositoryTests
{
    [Fact]
    public async Task MemoryRepo_CreateSingleEntity_EntityStored()
    {
        // Arrange
        var SUT = new MemoryRepository<MockEntity>();
        var entity = new MockEntity();

        // Act
        await SUT.AddAsync(entity);

        // Assert
        var createdEntity = await SUT.GetAsync(entity.PartitionKey, entity.RowKey);
        createdEntity.PartitionKey.Should().Be(entity.PartitionKey);
        createdEntity.RowKey.Should().Be(entity.RowKey);
        createdEntity.StringValue.Should().Be(entity.StringValue);
        createdEntity.IntValue.Should().Be(entity.IntValue);
        createdEntity.NullableBool.Should().Be(entity.NullableBool);
        createdEntity.DateValue.Should().Be(entity.DateValue);
    }

    [Fact]
    public async Task MemoryRepo_CreateMultipleEntity_MultipleEntityStored()
    {
        // Arrange
        var SUT = new MemoryRepository<MockEntity>();
        var entity1 = new MockEntity();
        var entity2 = new MockEntity();


        // Act
        await SUT.AddAsync(entity1);
        await SUT.AddAsync(entity2);


        // Assert
        var createdEntity1 = await SUT.GetAsync(entity1.PartitionKey, entity1.RowKey);
        var createdEntity2 = await SUT.GetAsync(entity2.PartitionKey, entity2.RowKey);

        createdEntity1.RowKey.Should().Be(entity1.RowKey);
        createdEntity2.RowKey.Should().Be(entity2.RowKey);
        createdEntity1.RowKey.Should().NotBe(createdEntity2.RowKey);
    }


    [Fact]
    public async Task MemoryRepo_CreateDuplicateEntity_ErrorThrown()
    {
        // Arrange
        var SUT = new MemoryRepository<MockEntity>();
        var entity = new MockEntity();

        // Act
        await SUT.AddAsync(entity);

        // Assert
        await Assert.ThrowsAsync<Exception>(async () => await SUT.AddAsync(entity));
    }
}

class MockEntity : BaseEntity
{
    public MockEntity() : base()
    {
        StringValue = "test";
        IntValue = 1;
        NullableBool = true;
        DateValue = new DateTime(2023, 1, 25);
    }

    public string StringValue { get; set; }
    public int IntValue { get; set; }
    public bool NullableBool { get; set; }
    public DateTime DateValue { get; set; }
}