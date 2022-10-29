using NeedleOrganizer.ViewModel;

namespace NeedleOrganizer.View;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(NeedlesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

