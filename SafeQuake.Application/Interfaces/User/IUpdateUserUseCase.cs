using SafeQuake.Domain.Entities;

namespace SafeQuake.Application.Interfaces.User
{
    public interface IUpdateUserUseCase
    {
        Task ExecuteAsync(UserEntity user);
    }
} 