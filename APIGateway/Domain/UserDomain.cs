using Domain.DTO;
using Domain.Interfaces.Domain;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;
        private readonly string _apiUserServiceUrl;

        public UserDomain(IUserService userService, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userService = userService;
            _httpClient = httpClientFactory.CreateClient();
            _apiUserServiceUrl = configuration.GetSection("urlAPIs:APIUserService").Value;
        }

        public async Task<string> Authenticate(UserCredentialsDTO credentials)
        {
            // Autenticar al usuario
            var IsOkLogin = await _userService.login(credentials.Username, credentials.Pass);
            // Validar Respuesta
            if (IsOkLogin)
            {
                return _userService.GenerateToken(credentials.Username);
            }
            else
            {
                return "Login Incorrecto";
            }
        }

        public async Task<string> GetUserData(UserCredentialsDTO credential, string token)
        {
            var url = $"{_apiUserServiceUrl}User/RegisterUser";
            var jsonContent = JsonSerializer.Serialize(credential);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Agregar el token como Bearer en el encabezado Authorization
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Error registering user");
            }
        }

        public Task<bool> Logout(string username)
        {
            return _userService.Logout(username);
        }

        public async Task<string> RegisterUser(UserRegistrationDTO credentials)
        {
            var url = $"{_apiUserServiceUrl}User/RegisterUser";
            var jsonContent = JsonSerializer.Serialize(credentials);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Error registering user");
            }
        }
    }
}
