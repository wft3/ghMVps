using Common.Models.UserManagement;
using Dashboard.Services.Interfaces;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dashboard.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            var _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient("ClientWithUntrustedSSL");
            _client.Timeout = TimeSpan.FromSeconds(240);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var response = await _client.GetFromJsonAsync<List<User>>("api/Users");

            return response;
        }
    }
}
