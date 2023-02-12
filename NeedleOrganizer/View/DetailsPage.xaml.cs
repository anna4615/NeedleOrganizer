using NeedleOrganizer.ViewModel;

namespace NeedleOrganizer.View;

public partial class DetailsPage : ContentPage
{

    public DetailsPage(NeedleDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}


