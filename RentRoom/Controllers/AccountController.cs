using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentRoom.DTO;
using RentRoom.Models;
using RentRoom.Services;


namespace RentRoom.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _JService;
        private readonly IEmailSendService _EmailService;
        public AccountController(UserManager<User> usermanager, SignInManager<User> signInManager,JwtService jwtService,IEmailSendService emailSend)
        {
            _JService = jwtService;
            _userManager = usermanager;
            _signInManager = signInManager;
            _EmailService = emailSend;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Email = await _userManager.FindByEmailAsync(registrationDTO.Email);
            if (Email != null)
            {
                return BadRequest("That email is used!");
            }

            try
            {
                var NewUser = new User
                {
                    Email = registrationDTO.Email,
                    UserName = registrationDTO.Username.ToLower(),
                    Role = "User"
                };

                var result = await _userManager.CreateAsync(NewUser, registrationDTO.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(NewUser);

                await _EmailService.sendAsync(registrationDTO.Email, code);

                Console.WriteLine(code);
                await _userManager.AddToRoleAsync(NewUser, "User");

                return Ok($"Please confirm your email with the code that you received!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            try
            {
                var User = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (User == null)
                {
                    return BadRequest();

                }
                if(!await _userManager.IsEmailConfirmedAsync(User))
                {
                    return Unauthorized();
                }
                 

                var result = await _signInManager.CheckPasswordSignInAsync(User, loginDTO.Password, false);

                if (!result.Succeeded)
                {
                    return BadRequest();
                }

                var token = _JService.CreateToken(User);

                var user = new NewUserDTO
                {
                    Email = User.Email,
                    UserName = User.UserName,
                    Token = token
                };

        
                return Ok(user);

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
                
            }

          
             

        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmation(string? email, string? code)
        {
            if (email == null || code == null)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest("Invalid payload!");
            var IsVerified = await _userManager.ConfirmEmailAsync(user, code);

            if(IsVerified.Succeeded)
            {
                return Ok("Confirmed succesfully!");
            }

            return BadRequest("Something wrong");
        }
       

    }
}
