using Mopups.Pages;
using newComparaYa.ViewModels;

namespace newComparaYa.Views;

public partial class FiltersModal : PopupPage
{
	public FiltersModal(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}