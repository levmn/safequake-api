using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.User
{
    public interface IGetUserUseCase
    {
        Task<UserEntity?> ExecuteAsync(int id);
    }
} 