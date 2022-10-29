using CommunityToolkit.Mvvm.Input;
using NeedleOrganizer.Interfaces;
using NeedleOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedleOrganizer.ViewModel
{
    public partial class NeedlesViewModel : BaseViewModel
    {

        private readonly INeedleService _needleService;

        public NeedlesViewModel(INeedleService needleService)
        {
            Title = "Stickor";
            _needleService = needleService;
            Needles = new ObservableCollection<Needle>();
        }

        public ObservableCollection<Needle> Needles { get; set; }

        [RelayCommand]
        async Task GetNeedlesAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                List<Needle> needles = await _needleService.GetNeedles();

                if (needles.Count != 0)
                {
                    Needles.Clear();
                    foreach (var needleSet in needles)
                    {
                        Needles.Add(needleSet);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
