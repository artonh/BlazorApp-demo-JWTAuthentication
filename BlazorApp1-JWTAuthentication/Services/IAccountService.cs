using BlazorApp1.JWTAuthentication.DTOs;
using static BlazorApp1.JWTAuthentication.Responses.CustomResponses;

namespace BlazorApp1.JWTAuthentication.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);
    }
}
