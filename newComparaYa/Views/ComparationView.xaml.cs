using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class ComparationView : ContentPage
{
	public ComparationView(MainViewModel viewModel)
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
            await vm.LoadComparationCommand.ExecuteAsync(null);
        }
    }
}