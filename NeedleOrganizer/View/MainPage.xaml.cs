using NeedleOrganizer.ViewModel;

namespace NeedleOrganizer.View;

public partial class MainPage : ContentPage
{

	public MainPage(NeedlesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}

