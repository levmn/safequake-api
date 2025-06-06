using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.User
{
    public interface ICreateUserUseCase
    {
        Task ExecuteAsync(UserEntity user);
    }
} 