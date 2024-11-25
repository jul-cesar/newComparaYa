using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class UserView : ContentPage
{
	public UserView(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}