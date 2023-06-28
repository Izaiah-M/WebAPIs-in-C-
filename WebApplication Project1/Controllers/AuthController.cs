using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Project1.DTOs;
using WebApplication_Project1.IRepository;
using WebApplication_Project1.Models;

namespace WebApplication_Project1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {

            _logger.LogInformation($"Registering user. Firstname: {userDTO.FirstName} LastName: {userDTO.LastName} Email: {userDTO.Email}");

            if (userDTO.Email == null || userDTO.Password == null || userDTO.FirstName == null || userDTO.LastName == null)
            {
                _logger.LogInformation($"Missing Fields Firstname: {userDTO.FirstName} LastName: {userDTO.LastName} Email: {userDTO.Email} password: {userDTO.Password}");
                return BadRequest("Missing Fields");
            }

            // This here will go ahead to see other requirements I made in the user DTO and varify them
            if(!ModelState.IsValid)
            {
                _logger.LogInformation($"Missing Fields Firstname: {userDTO.FirstName} LastName: {userDTO.LastName} Email: {userDTO.Email} password: {userDTO.Password}");
                return BadRequest(ModelState);
            }

            try
            {

                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email.Split('@')[0]; // Because Identity always expects a username, but we are using email as defaule username
                var result = await _userManager.CreateAsync(user, userDTO.Password); // using Idenetity core, it will take the user...hash the password and do all the necessary stuff before we store the user in the database

                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                    _logger.LogInformation($"User registration failed:  {error.Code}: {error.Description}");

                    _logger.LogInformation($"Username {user.UserName} firstname: {user.FirstName}");
                    }
                    return BadRequest("Something went wrong");
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);

                return Ok("User successfully registered");

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Something went wrong, {nameof(Register)} ", ex);

                return Problem($"Something went wrong, {nameof(Register)}", statusCode: 500);
            }  
        }
        /*
                [HttpPost]
                [Route("login")]
                public async Task<ActionResult> Login([FromBody] LoginDTO userDTO)
                {
                    _logger.LogInformation($"Logging in; Email: {userDTO.Email}, Password: {userDTO.Password}");

                    if (userDTO.Email == null || userDTO.Password == null)
                    {
                        _logger.LogInformation($"Missing Fields Email: {userDTO.Email} password: {userDTO.Password}");
                        return BadRequest("Missing fields");
                    }

                    // This here will go ahead to see other requirements I made in the user DTO and varify them
                    if (!ModelState.IsValid)
                    {
                        _logger.LogInformation($"Invalid fields Email: {userDTO.Email} password: {userDTO.Password}");
                        return BadRequest(ModelState);
                    }

                    try
                    {
                        // we are going to use the signin manager to help sign in
                        var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);

                        if (!result.Succeeded)
                        {
                            _logger.LogInformation($"User registration failed Email: {userDTO.Email}, Password: {userDTO.Password}");

                            return Unauthorized("Invalid Password or Username");
                        }

                        _logger.LogInformation($"User: {userDTO.Email} signed in");

                        return Ok("User successfully signed in");

                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Something went wrong, {nameof(Login)} ", ex);

                        return Problem($"Something went wrong, {nameof(Login)}", statusCode: 500);
                    }
                }*/

    }
}
