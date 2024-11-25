

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using newComparaYa.Models;
using newComparaYa.Services;

namespace newComparaYa.ViewModels;

public partial class AuthViewModel : ObservableObject
{
    private readonly AuthService authService;

    public AuthViewModel(AuthService auth)
    {
        authService = auth;

    }


    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]

    private string errorMessage;

    [ObservableProperty]
    private string passwordEntry;

    [ObservableProperty]
    private string emailEntry;
    [ObservableProperty]
    private string nombreEntry;




    [RelayCommand]
    public async Task ToLogin()
    {
        await Shell.Current.GoToAsync("login");
    }


    [RelayCommand]
    public async Task ToRegister()
    {
        await Shell.Current.GoToAsync("register");
    }

    [RelayCommand]
    public async Task LoginUser()
    {
        if (string.IsNullOrWhiteSpace(PasswordEntry) || string.IsNullOrWhiteSpace(EmailEntry))
        {
            Console.WriteLine("Please fill in all fields.");
            ErrorMessage = "Por favor, llena todos los campos";
            IsLoading = false;
            return;
        }

        IsLoading = true;

        try
        {
            var loginData = new Logeo
            {
                email = EmailEntry,
                password = PasswordEntry,
            };

            var response = await authService.LoginUserService(loginData);

            if (response != null && !string.IsNullOrEmpty(response.token) && !string.IsNullOrEmpty(response.data.id_Usuario) && !string.IsNullOrEmpty(response.data.nombre) && !string.IsNullOrEmpty(response.data.email))
            {
                Console.WriteLine("Login successful");
                await SecureStorage.SetAsync("token", response.token);
                await SecureStorage.SetAsync("id", response.data.id_Usuario);
                await SecureStorage.SetAsync("email", response.data.email);
                await SecureStorage.SetAsync("nombre", response.data.nombre);


                ErrorMessage = string.Empty;
                EmailEntry = string.Empty;
                PasswordEntry = string.Empty;

                await Shell.Current.GoToAsync("//products", true);

            }
            else
            {
                Console.WriteLine("Login failed.");
                ErrorMessage = "No se pudo iniciar sesión. Intenta nuevamente.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task RegisterUser()
    {
        if (string.IsNullOrWhiteSpace(PasswordEntry) || string.IsNullOrWhiteSpace(EmailEntry) || string.IsNullOrWhiteSpace(NombreEntry))
        {
            Console.WriteLine("Please fill in all fields.");
            ErrorMessage = "Por favor, llena todos los campos";
            IsLoading = false;
            return;
        }

        IsLoading = true;

        try
        {
            var registroData = new Registro
            {
                email = EmailEntry,
                password = PasswordEntry,
                name = NombreEntry
            };

            var response = await authService.RegisterUserService(registroData);

            if (response != null )
            {
                Console.WriteLine("Register successful");
              


                ErrorMessage = string.Empty;
                EmailEntry = string.Empty;
                PasswordEntry = string.Empty;

                await Shell.Current.GoToAsync("//login", true);

            }
            else
            {
                Console.WriteLine("Register failed.");
                ErrorMessage = "No se pudo registrar. Intenta nuevamente.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during register: {ex.Message}");
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

}



