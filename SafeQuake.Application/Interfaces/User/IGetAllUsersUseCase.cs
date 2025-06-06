using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.User
{
    public interface IGetAllUsersUseCase
    {
        Task<IEnumerable<UserEntity>> ExecuteAsync();
    }
} 