using BlazorApp1.JWTAuthentication.DTOs;
using BlazorApp1.JWTAuthentication.Repos;
using Microsoft.AspNetCore.Mvc;
using static BlazorApp1.JWTAuthentication.Responses.CustomResponses;

namespace BlazorApp1.JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountRepo;
        public AccountController(IAccount account)
        {
            accountRepo = account;
        }


        [HttpPost, Route("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO model)
        {
            var result = await accountRepo.RegisterAsync(model);
            return Ok(result);
        }


        [HttpPost, Route("login")]
        public async Task<ActionResult<RegistrationResponse>> LoginAsync(LoginDTO model)
        {
            var result = await accountRepo.LoginAsync(model);
            return Ok(result);
        }

    }
}
