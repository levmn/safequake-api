using SafeQuake.Domain.Entities;

namespace SafeQuake.Domain.Requests
{
    public class UserRequest
    {
        public int? Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }

        public UserEntity GetUserEntity()
        {
            return new UserEntity
            {
                Id = this.Id?? 0,
                Name = this.Name,
                Email = this.Email,
                Password = this.Password,
                Address = this.Address
            };
        }
    }
}