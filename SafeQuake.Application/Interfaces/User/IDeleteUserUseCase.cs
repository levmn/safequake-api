namespace SafeQuake.Application.Interfaces.User
{
    public interface IDeleteUserUseCase
    {
        Task ExecuteAsync(int id);
    }
} 