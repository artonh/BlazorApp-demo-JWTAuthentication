using BlazorApp1.JWTAuthentication.DTOs;
using static BlazorApp1.JWTAuthentication.Responses.CustomResponses;

namespace BlazorApp1.JWTAuthentication.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResponse> LoginAsync(LoginDTO model);

    }
}
