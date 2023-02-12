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

    [QueryProperty("Needle", "Needle")]
    public partial class NeedleDetailsViewModel : BaseViewModel
    {

        public NeedleDetailsViewModel()
        {
        }

        [ObservableProperty]
        Needle needle;


        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
