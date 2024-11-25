using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using newComparaYa.Models;
using newComparaYa.Service;
using newComparaYa.Services;
using newComparaYa.Views;

namespace newComparaYa.ViewModels;

[QueryProperty(nameof(IdProduct), "productId")]
public partial class MainViewModel : ObservableObject
{
    private readonly MainService mainService;
    private readonly ComparationService comparationService;
    private readonly IServiceProvider provider;

    public MainViewModel(MainService services, ComparationService comp, IServiceProvider provider)
    {
        mainService = services;
        comparationService = comp;
        this.provider = provider;
      


    }

    [ObservableProperty]
    private ObservableCollection<Product> productsList = new();
    [ObservableProperty]
    private ObservableCollection<Product> cartProducts = new();
    [ObservableProperty]

    private ObservableCollection<Product> favs = new();

    [ObservableProperty]
    private ObservableCollection<Category> categoriesList = new();

    [ObservableProperty]
    private ObservableCollection<Product> similarProducts = new();
    [ObservableProperty]
    private ObservableCollection<Product> relatedProducts = new();

    [ObservableProperty]
    private string idProduct;


    [ObservableProperty]
    private string nombreUser;


    [ObservableProperty]
    private string emailUser;


    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private int currentPage = 1;

    [ObservableProperty]
    private int limit = 20;

    [ObservableProperty]
    private bool hasMoreItems = true;

    [ObservableProperty]
    private Category selectedCategory;

    [ObservableProperty]
    private string searchQuery;

    [ObservableProperty]
    private decimal totalExito;

    [ObservableProperty]
    private decimal totalD1;

    [ObservableProperty]
    private decimal totalOlimpica;

   

    [ObservableProperty]
    private decimal totalNet;
    [ObservableProperty]
    private ObservableCollection<string> items = new ObservableCollection<string>
    {
        "50000", "30000", "20000", "10000", "5000"
    };

    // ObservableCollection para distris
    [ObservableProperty]
    private ObservableCollection<string> distris = new ObservableCollection<string>
    {
        "D1", "OLIMPICA", "EXITO"
    };


    [ObservableProperty]

    private string selectedPrice;

    [ObservableProperty]
    private string selectedDistributor;

    [RelayCommand]
    public async Task LoadUserData()
    {
        NombreUser = await SecureStorage.GetAsync("nombre");
            EmailUser = await SecureStorage.GetAsync("email");
    }

    [RelayCommand]
    public async Task Logout()
    {
         SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync("login");
    }

    [RelayCommand]
    public async void PriceFilterSelected()
    {
        await ApplyFiltersAsync();
        await MopupService.Instance.PopAsync();


    }

    [RelayCommand]
    public async Task ApplyFiltersAsync()
    {
        // Call the API with selected filters


        // Replace this with your API call logic
       var response = await mainService.GetProductsService(CurrentPage, Limit, "", SelectedPrice);
        var products = response.data;
        ProductsList.Clear();
        if (products.Any())
        {
            foreach (var product in products)
            {
                ProductsList.Add(product);
            }
            CurrentPage++;
        }
        SelectedCategory = null;
    }

    [RelayCommand]
    public async Task LoadComparation(string? query)
    {
        if (IsLoading)
            return;

        IsLoading = true;

        try
        {
            var response = await comparationService.GetProductsComparation(IdProduct);
            var similar = response.similarProducts;
            var related = response.relatedProducts;
            RelatedProducts.Clear();
            SimilarProducts.Clear();
            if (similar.Any())
            {
                foreach (var product in similar)
                {
                    SimilarProducts.Add(product);
                }
                if (related.Any())
                {
                    foreach (var product in related)
                    {
                        RelatedProducts.Add(product);
                    }
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar productos: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task SelectionCategory()
    {
        if (SelectedCategory == null)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            // Reiniciar estados para nueva categoría
            CurrentPage = 1;
            HasMoreItems = true;

            ProductsList.Clear();

            var response = await mainService.GetProductsByCategoryService(CurrentPage, Limit, SelectedCategory.id);
            var products = response.data;

            if (products.Any())
            {
                foreach (var product in products)
                {
                    ProductsList.Add(product);
                }
            }
            else
            {
                HasMoreItems = false;
                Toast.Make($"No hay productos en la categoría {SelectedCategory.name}.").Show();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al cargar productos de la categoría: {ex.Message}";
            Toast.Make(ErrorMessage).Show();
        }
        finally
        {
            IsLoading = false;
            SelectedCategory = null; // Restablecer la selección
        }
    }

    [RelayCommand]
    public async Task LoadProductsAsync(string? query)
    {
        if (!HasMoreItems || IsLoading)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var response = await mainService.GetProductsService(CurrentPage, Limit, query, "0");
            var products = response.data;

            if (products.Any())
            {
                foreach (var product in products)
                {
                    ProductsList.Add(product);
                }
                CurrentPage++;
            }
            else
            {
                HasMoreItems = false; // No hay más elementos
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al cargar productos: {ex.Message}";
            Toast.Make(ErrorMessage).Show();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task LoadCategories()
    {
        if (IsLoading)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var categories = await mainService.GetCategoriesService();

            CategoriesList.Clear();

            if (categories.Any())
            {
                foreach (var category in categories)
                {
                    CategoriesList.Add(category);
                }
            }
            else
            {
                Toast.Make("No se encontraron categorías.").Show();
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al cargar categorías: {ex.Message}";
            Toast.Make(ErrorMessage).Show();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task Search()
    {
        // Reiniciar estados para una nueva búsqueda
        CurrentPage = 1;
        HasMoreItems = true;
        ProductsList.Clear();

        await LoadProductsAsync(SearchQuery); // Realizar la búsqueda con el texto actual
    }

    private void UpdateTotals()
    {
        TotalExito = CartProducts.Sum(p => p.Cantidad * (p.price_exito ?? 0));
        TotalD1 = CartProducts.Sum(p => p.Cantidad * (p.price_d1 ?? 0));
        TotalOlimpica = CartProducts.Sum(p => p.Cantidad * (p.price_olim ?? 0));
        TotalNet = TotalExito + TotalD1 + TotalOlimpica;
    }

    [RelayCommand]
    public void RemoveProduct(Product product)
    {
        if (product != null && CartProducts.Contains(product))
        {
            CartProducts.Remove(product);
            UpdateTotals();
        }
    }

    [RelayCommand]
    public void IncreaseQuantity(Product product)
    {
        if (product != null)
        {
            product.Cantidad++;
            UpdateTotals();
        }
    }

    [RelayCommand]
    public void DecreaseQuantity(Product product)
    {
        if (product != null && product.Cantidad > 1)
        {
            product.Cantidad--;
            UpdateTotals();
        }
    }

    [RelayCommand]

    public async Task OpenFilters()
    {
        await MopupService.Instance.PushAsync(provider.GetRequiredService<FiltersModal>());

    }



    [RelayCommand]
    public async Task Compare(string productId)
    {
        if (string.IsNullOrEmpty(productId))
            return;

        // Aquí manejas lo que haces con el ID del producto
        Console.WriteLine($"Producto seleccionado: {productId}");
        await Shell.Current.GoToAsync($"comparation?productId={productId}");
    }

    [RelayCommand]
    public Task AddFav(Product product)
    {
        if (product != null && !Favs.Contains(product))
        {
            Favs.Add(product);
            Toast.Make("Producto agregado a favoritos").Show();
        }
        else
        {
            Toast.Make("Este producto ya se encuentra en favoritos").Show();
        }
        UpdateTotals();

        return Task.CompletedTask;
    }

    [RelayCommand]
    public Task AddProduct(Product product)
    {
        if (product != null && !CartProducts.Contains(product))
        {
            CartProducts.Add(product);
            Toast.Make("Producto agregado al carrito correctamente").Show();
        }
        else
        {
            Toast.Make("Este producto ya se encuentra en el carrito").Show();
        }
        UpdateTotals();

        return Task.CompletedTask;
    }

}

