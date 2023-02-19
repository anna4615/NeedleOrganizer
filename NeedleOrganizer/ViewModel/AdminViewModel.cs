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
    public partial class AdminViewModel : BaseViewModel
    {

        public AdminViewModel()
        {
            Title = "Adminverktyg";
        }


        [RelayCommand]
        public void DeleteAllNeedles()
        {
            Utils.DeleteNeedlesFile();
        }


        [RelayCommand]
        public async Task AddDefaultNeedles()
        {
            await Utils.CreateDefaultNeedles();
        }



        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
