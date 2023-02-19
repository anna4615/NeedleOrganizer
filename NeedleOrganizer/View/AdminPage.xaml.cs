using NeedleOrganizer.ViewModel;

namespace NeedleOrganizer.View;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}