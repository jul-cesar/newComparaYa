using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class FavsView : ContentPage
{
	public FavsView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}