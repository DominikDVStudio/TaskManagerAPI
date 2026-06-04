using Moq;
using TaskManagerApi.Application.Exceptions;
using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Application.Queries.TaskItems;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Tests.QueryHandlers.Tasks;

public class GetTaskByIdQueryHandlerTests
{
    [Fact]
    public async Task GetTaskByIdQueryHandler_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(1))
            .ReturnsAsync((TaskItem?)null);

        var queryHandler = new GetTaskByIdQueryHandler(repositoryMock.Object);

        var query = new GetTaskByIdQuery
        {
            Id = 1
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => queryHandler.Execute(query));
    }

    [Fact]
    public async Task GetTaskByIdQueryHandler_ShouldThrowForbiddenException_WhenTaskBelongsToAnotherUser()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(1))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Task",
                Description = "Test",
                UserId = 1
            });

        var queryHandler = new GetTaskByIdQueryHandler(repositoryMock.Object);

        var query = new GetTaskByIdQuery
        {
            Id = 1,
            CurrentUserId = 2
        };

        await Assert.ThrowsAsync<ForbiddenException>(() => queryHandler.Execute(query));
    }

    [Fact]
    public async Task GetTaskByIdQueryHandler_ShouldReturnTask_WhenTaskExistsAndBelongsToUser()
    {
        var repositoryMock = new Mock<ITaskRepository>();

        repositoryMock
            .Setup(r => r.GetTaskByIdAsync(1))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Task",
                Description = "Test",
                UserId = 1
            });

        var queryHandler = new GetTaskByIdQueryHandler(repositoryMock.Object);

        var query = new GetTaskByIdQuery
        {
            Id = 1,
            CurrentUserId = 1
        };

        var result = await queryHandler.Execute(query);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Task", result.Title);
        Assert.Equal("Test", result.Description);

        repositoryMock.Verify(
            r => r.GetTaskByIdAsync(It.IsAny<int>()),
            Times.Once);
    }
}