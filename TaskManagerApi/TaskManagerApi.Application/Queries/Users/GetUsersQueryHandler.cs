using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.Users;

public class GetUsersQueryHandler
{
    private readonly IUserRepository _repository;
    
    public GetUsersQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<User>> Execute(GetUsersQuery query)
    {
        return await _repository.GetAllUsersAsync();
    }
}