
using System.Net.Http.Json;
using System.Text;
using newComparaYa.Models;
using Newtonsoft.Json;


namespace newComparaYa.Services
{
    public class AuthService
    {
        public HttpClient Client { get; set; }


        public AuthService()
        {
            Client = new HttpClient();
        }
     
        public async Task<LoginResponse> LoginUserService(Logeo data)
        {
            var apiUrl = $"{Constants.API_URL}/api/auth/login";
            var jsonContent = JsonConvert.SerializeObject(data);
            Console.WriteLine($"JSON Content: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return responseData;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Content: {errorContent}");

                var errorResponse = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorContent);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.message))
                {
                    throw new Exception(errorResponse.message);
                }

                throw new Exception("An unknown error occurred during login.");
            }
        }

        public async Task<RegistroResponse> RegisterUserService(Registro data)
        {
            var apiUrl = $"{Constants.API_URL}/api/auth/register";
            var jsonContent = JsonConvert.SerializeObject(data);
            Console.WriteLine($"JSON Content: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<RegistroResponse>();
                return responseData;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Content: {errorContent}");

                var errorResponse = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorContent);
                if (errorResponse != null && !string.IsNullOrEmpty(errorResponse.message))
                {
                    throw new Exception(errorResponse.message);
                }

                throw new Exception("An unknown error occurred during register.");
            }
        }
    }
}
