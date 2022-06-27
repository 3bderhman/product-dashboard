using ECommerce.BL.Helper;
using ECommerce.BL.Model;
using ECommerce.DAL.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Jwt jwt;

        public AccountController(UserManager<ApplicationUser> userManager, IOptions<Jwt> jwt, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwt = jwt.Value;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            if (ModelState.IsValid)
            {
                var users = await userManager.FindByEmailAsync(model.Email);
                if (users != null)
                {
                    return Ok(new ResponsiveMessage<string>
                    {
                        Code = "409",
                        Status = "Conflict",
                        Message = "This E-mail is Already Exited Create Another E-mail",
                        Data = "This E-mail is Already Exited Create Another E-mail",
                    });
                }
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var ErrorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        ErrorList.Add(error.Description);
                    }
                    return Ok(new ResponsiveMessage<List<string>>
                    {
                        Code = "500",
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again.",
                        Data = ErrorList
                    });
                }
                return Ok(new ResponsiveMessage<string>
                {
                    Code = "201",
                    Status = "Success",
                    Message = "User created successfully!",
                    Data = "success"
                });
            }
            return ValidationProblem();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync (model.UserName);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var UserRoles = await userManager.GetRolesAsync(user);
                    var AuthClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    foreach (var UserRole in UserRoles)
                        AuthClaims.Add(new Claim(ClaimTypes.Role, UserRole));
                        var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
                        var Token = new JwtSecurityToken(
                        issuer: jwt.ValidIssuer,
                        audience: jwt.ValidAudience,
                        expires: DateTime.Now.AddHours(jwt.DurationInDays),
                        claims: AuthClaims,
                        signingCredentials: new SigningCredentials(Authkey, SecurityAlgorithms.HmacSha256)
                    );
                    return Ok(new
                    {
                        Message = "success",
                        token = new JwtSecurityTokenHandler().WriteToken(Token),
                        expiration = Token.ValidTo
                    });
                }
                return Unauthorized();
            }
            return ValidationProblem();
        }
        [HttpPost]
        [Route("~/api/Account/SingOut")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok(new ResponsiveMessage<string>
            {
                Code = "200",
                Status = "Ok",
                Message = "Success",
                Data = "SignOut"
            });
        }
    }
}
