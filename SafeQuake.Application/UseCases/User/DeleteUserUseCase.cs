using Microsoft.EntityFrameworkCore;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.User
{
    public class DeleteUserUseCase(IAppDbContext context) : IDeleteUserUseCase
    {
        public async Task ExecuteAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
} 