using Moq;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.DeleteTask;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Tests;

public class DeleteTaskUseCaseTests
{
    [Fact]
    public async Task Should_Throw_Exception_When_Task_Does_Not_Exist()
    {
        // Arrange
        var repoMock = new Mock<ITaskRepository>();

        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TaskItem?)null);

        var useCase = new DeleteTaskUseCase(repoMock.Object);

        var command = new DeleteTaskCommand
        {
            Id = Guid.NewGuid()
        };

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() => useCase.Execute(command));
    }
    
    [Fact]
    public async Task Should_Call_Delete_When_Task_Exists()
    {
        // Arrange
        var repoMock = new Mock<ITaskRepository>();

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Test"
        };

        repoMock
            .Setup(r => r.GetByIdAsync(task.Id))
            .ReturnsAsync(task);

        var useCase = new DeleteTaskUseCase(repoMock.Object);

        var command = new DeleteTaskCommand
        {
            Id = task.Id
        };

        // Act
        await useCase.Execute(command);

        // Assert
        repoMock.Verify(r => r.DeleteAsync(task.Id), Times.Once);
    }
}