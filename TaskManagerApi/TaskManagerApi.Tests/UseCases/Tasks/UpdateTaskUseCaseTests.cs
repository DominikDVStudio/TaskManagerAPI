using Moq;
using TaskManagerApi.Application.Commands.TaskItem;
using TaskManagerApi.Application.Exceptions;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.UseCases.TaskItems.UpdateTask;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Tests.UseCases.Tasks;

public class UpdateTaskUseCaseTests
{
    [Fact]
    public async Task UpdateTaskUseCase_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((TaskItem?)null);

        var useCase = new UpdateTaskUseCase(repositoryMock.Object);

        var command = new UpdateTaskCommand
        {
            Id = 1,
            Title = "Updated",
            Description = "Updated",
            IsDone = true,
            CurrentUserId = 1
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => useCase.Execute(command));
    }

    [Fact]
    public async Task UpdateTaskUseCase_ShouldThrowForbiddenException_WhenTaskBelongsToAnotherUser()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                UserId = 999
            });

        var useCase = new UpdateTaskUseCase(repositoryMock.Object);

        var command = new UpdateTaskCommand
        {
            Id = 1,
            Title = "Updated",
            Description = "Updated",
            CurrentUserId = 1
        };

        await Assert.ThrowsAsync<ForbiddenException>(() => useCase.Execute(command));
    }

    [Fact]
    public async Task UpdateTaskUseCase_TaskShouldUpdateTask_WhenTaskExistsAndBelongsToUser()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Test",
                Description = "Test",
                IsDone = false,
                UserId = 999
            });

        var useCase = new UpdateTaskUseCase(repositoryMock.Object);

        var command = new UpdateTaskCommand
        {
            Id = 1,
            Title = "Updated",
            Description = "Updated",
            IsDone = true,
            CurrentUserId = 999
        };

        await useCase.Execute(command);

        repositoryMock.Verify(
            r => r.UpdateTaskAsync(
                It.Is<TaskItem>(t =>
                    t.Title == "Updated" &&
                    t.Description == "Updated" &&
                    t.IsDone == true)),
            Times.Once);
    }
}