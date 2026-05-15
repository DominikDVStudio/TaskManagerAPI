using Moq;
using TaskManagerApi.Application.Commands;
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
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((TaskItem?)null);

        var useCase = new DeleteTaskUseCase(repoMock.Object);

        var command = new DeleteTaskCommand
        {
           Id = It.IsAny<int>()
        };

        // Act + Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => useCase.Execute(command)
        );
    }
    
    [Fact]
    public async Task Should_Call_Delete_When_Task_Exists()
    {
        // Arrange
        var repoMock = new Mock<ITaskRepository>();

        var task = new TaskItem
        {
            Id = It.IsAny<int>(),
            Title = "Test"
        };

        repoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
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