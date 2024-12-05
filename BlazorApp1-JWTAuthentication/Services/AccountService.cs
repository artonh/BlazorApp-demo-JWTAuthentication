using BlazorApp1.JWTAuthentication.DTOs;
using BlazorApp1.JWTAuthentication.Responses;
using BlazorApp1.JWTAuthentication.States;
using System.Reflection.Metadata;

namespace BlazorApp1.JWTAuthentication.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomResponses.LoginResponse> LoginAsync(LoginDTO model)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/account/login", model);
                var result = await response.Content.ReadFromJsonAsync<CustomResponses.LoginResponse>();

                return result!;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CustomResponses.RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            if(Constants.JWTToken == null)
                return null;

            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            var response = await httpClient.PostAsJsonAsync("api/account/register", model);
            var result = await response.Content.ReadFromJsonAsync<CustomResponses.RegistrationResponse>();

            return result!;
        }
    }
}
