using Microsoft.EntityFrameworkCore;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.User
{
    public class GetUserUseCase(IAppDbContext context) : IGetUserUseCase
    {
        public async Task<UserEntity?> ExecuteAsync(int id)
        {
            return await context.Users
                .Include(u => u.EarthquakeEvents)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
} 