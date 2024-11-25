

using newComparaYa.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace newComparaYa.Services;

public class ComparationService
{
    public HttpClient Client { get; set; }

    public ComparationService()
    {
        Client = new HttpClient();
    }

    public async Task<Comparation> GetProductsComparation(string idProduct)
    {


        var apiUrl = $"{Constants.API_URL}/api/products/comparation/{idProduct}";


        // Recupera el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agrega el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await Client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Leer el contenido de la respuesta como string
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserializar usando Newtonsoft.Json
            var responseData = JsonConvert.DeserializeObject<Comparation>(jsonResponse);

            if (responseData != null)
            {
                Console.WriteLine(JsonConvert.SerializeObject(responseData, Formatting.Indented));
                return responseData;
            }
            else
            {
                throw new Exception("La respuesta fue exitosa, pero los datos están vacíos.");
            }
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener los productos. Código de estado: {response.StatusCode}. Detalle: {errorContent}");
        }
    }

}

