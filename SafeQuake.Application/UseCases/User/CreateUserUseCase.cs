using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.User
{
    public class CreateUserUseCase(IAppDbContext context) : ICreateUserUseCase
    {
        public async Task ExecuteAsync(UserEntity user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
} 