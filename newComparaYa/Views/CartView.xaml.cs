using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class CartView : ContentPage
{
	public CartView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}