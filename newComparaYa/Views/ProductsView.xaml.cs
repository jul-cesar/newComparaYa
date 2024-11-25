using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class ProductsView : ContentPage
{
	public ProductsView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	

	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as MainViewModel;
        if (vm != null)
        {
            await vm.LoadProductsCommand.ExecuteAsync(null);

            await vm.LoadCategoriesCommand.ExecuteAsync(null);
            await vm.LoadUserDataCommand.ExecuteAsync(null);
            if (string.IsNullOrEmpty(vm.EmailUser))
            {
                await Shell.Current.GoToAsync("login");
            }
        }
        cvPro.RemainingItemsThresholdReached += async (sender, e) =>
        {
            await vm.LoadProductsCommand.ExecuteAsync(null);
        };
        cvPro.RemainingItemsThreshold = 2;
    }
}