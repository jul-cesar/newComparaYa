using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class LoginView : ContentPage
{
	public LoginView(AuthViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}