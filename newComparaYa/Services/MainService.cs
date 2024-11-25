

using newComparaYa.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace newComparaYa.Service;

public class MainService
{
    public HttpClient Client { get; set; }

    public MainService()
    {
        Client = new HttpClient();

    }

    public async Task<ProductsReponse> GetProductsService(int page, int limit, string? query, string? price)
    {
        // Construir la URL base
        var apiUrl = $"{Constants.API_URL}/api/products?page={page}&limit={limit}";

        // Añadir el parámetro de búsqueda si existe
        if (!string.IsNullOrEmpty(query))
        {
            apiUrl += $"&search={Uri.EscapeDataString(query)}";
        }

        // Añadir el parámetro de precio si existe
        if (!string.IsNullOrEmpty(price))
        {
            apiUrl += $"&price={price}";
        }

        // Recuperar el token de SecureStorage
        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("No se encontró el token de autenticación.");
        }

        // Agregar el token en el encabezado de autorización
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            // Enviar la solicitud GET
            var response = await Client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta como string
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar usando Newtonsoft.Json
                var responseData = JsonConvert.DeserializeObject<ProductsReponse>(jsonResponse);

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
        catch (Exception ex)
        {
            throw new Exception($"Error al realizar la solicitud: {ex.Message}");
        }
    }


    public async Task<ProductsReponse> GetProductsByCategoryService(int page, int limit, string categoryId)
    {
        var apiUrl = $"{Constants.API_URL}/api/products?page={page}&limit={limit}&category_id={categoryId}";

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
            var responseData = JsonConvert.DeserializeObject<ProductsReponse>(jsonResponse);

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



    public async Task<List<Category>> GetCategoriesService()
    {
        var apiUrl = $"{Constants.API_URL}/api/categories";

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
            var responseData = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);

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
            throw new Exception($"Error al obtener las categorias. Código de estado: {response.StatusCode}. Detalle: {errorContent}");
        }
    }

}

