using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(AuthViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}
}