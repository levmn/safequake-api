using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.User
{
    public class UpdateUserUseCase(IAppDbContext context) : IUpdateUserUseCase
    {
        public async Task ExecuteAsync(UserEntity user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
} 