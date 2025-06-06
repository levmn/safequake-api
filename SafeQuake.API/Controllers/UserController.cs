using Microsoft.AspNetCore.Mvc;
using SafeQuake.Application.Interfaces.User;
using SafeQuake.Domain.Entities;

namespace SafeQuake.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly IGetAllUsersUseCase _getAllUsersUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;

        public UserController(
            ICreateUserUseCase createUserUseCase,
            IGetUserUseCase getUserUseCase,
            IGetAllUsersUseCase getAllUsersUseCase,
            IUpdateUserUseCase updateUserUseCase,
            IDeleteUserUseCase deleteUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _getUserUseCase = getUserUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
            _updateUserUseCase = updateUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _createUserUseCase.ExecuteAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Obtém um usuário pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserEntity>> GetById(int id)
        {
            var user = await _getUserUseCase.ExecuteAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAll()
        {
            var users = await _getAllUsersUseCase.ExecuteAsync();
            return Ok(users);
        }

        /// <summary>
        /// Atualiza as informações de um usuário
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UserEntity user)
        {
            if (id != user.Id)
                return BadRequest();

            var existingUser = await _getUserUseCase.ExecuteAsync(id);
            if (existingUser == null)
                return NotFound();

            await _updateUserUseCase.ExecuteAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _getUserUseCase.ExecuteAsync(id);
            if (existingUser == null)
                return NotFound();

            await _deleteUserUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
} 