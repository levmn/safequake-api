using Microsoft.EntityFrameworkCore;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;
using SafeQuake.Application.Interfaces.Data;

namespace SafeQuake.Application.UseCases.User
{
    public class GetAllUsersUseCase(IAppDbContext context) : IGetAllUsersUseCase
    {
        public async Task<IEnumerable<UserEntity>> ExecuteAsync()
        {
            return await context.Users
                .Include(u => u.EarthquakeEvents)
                .ToListAsync();
        }
    }
} 