using TaskManagerApi.Application.Interfaces;
using TaskManagerApi.Domain.Entities;

namespace TaskManagerApi.Application.Queries.Users;

public class GetUserByIdQueryHandler
{
    private readonly IUserRepository _repository;
    
    public GetUserByIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<User?> Execute(GetUserByIdQuery query)
    {
        return await _repository.GetUserByIdAsync(query.Id);
    }
}