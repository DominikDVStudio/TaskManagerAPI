using Moq;
using TaskManagerApi.Application.Commands;
using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.TaskItems.CreateTask;

namespace TaskManagerApi.Tests;

public class CreateTaskUseCaseTests
{
    [Fact]
    public async Task Should_Throw_Exception_When_Title_Is_Empty()
    {
        // Arrange
        var repoMock = new Mock<ITaskRepository>();
    
        var useCase = new CreateTaskUseCase(repoMock.Object);
    
        var command = new CreateTaskCommand
        {
            Title = "",
            Description = "Test"
        };
        
        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => useCase.Execute(command)
        );
     }
}