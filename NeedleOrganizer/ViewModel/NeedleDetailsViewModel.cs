using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NeedleOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedleOrganizer.ViewModel
{

    [QueryProperty("ViewNeedle", "ViewNeedle")]
    public partial class NeedleDetailsViewModel : BaseViewModel
    {

        public NeedleDetailsViewModel()
        {
        }

        [ObservableProperty]
        ViewNeedle viewNeedle;


        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
