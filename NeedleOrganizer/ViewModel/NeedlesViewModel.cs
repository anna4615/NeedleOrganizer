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
            SelectedNeedles = new ObservableCollection<ViewNeedle>();
        }

        public ObservableCollection<ViewNeedle> SelectedNeedles { get; set; }
        public List<Needle> NeedlesFromDataStorage { get; set; }

        [RelayCommand]
        async Task GetAllNeedlesAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            if (NeedlesFromDataStorage == null)
            {
                try
                {
                    NeedlesFromDataStorage = await _needleService.GetNeedles();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await Shell.Current.DisplayAlert("Error!", $"Kunde inte hämta stickor: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

            PopulateSelectedNeedles(NeedlesFromDataStorage);

            IsBusy = false;
        }


        [RelayCommand]
        async Task GetCircularNeedlesAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            if (NeedlesFromDataStorage == null)
            {
                try
                {
                    NeedlesFromDataStorage = await _needleService.GetNeedles();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    await Shell.Current.DisplayAlert("Error!", $"Kunde inte hämta stickor: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

            PopulateSelectedNeedles(NeedlesFromDataStorage.Where(n => n.Type.ToLower() == "Rundsticka".ToLower()).ToList());

            IsBusy = false;
        }



        private void PopulateSelectedNeedles(List<Needle> needles)
        {
            SelectedNeedles.Clear();

            foreach (var needleSet in needles)
            {
                ViewNeedle viewNeedle = new ViewNeedle
                {
                    Type = needleSet.Type,
                    Size = needleSet.Size.ToString() + " mm",
                    Length = needleSet.Length.ToString() + " cm",
                    HasLength = needleSet.Length != null,
                    Manufacturer = needleSet.Manufacturer,
                    Image = needleSet.Image
                };

                SelectedNeedles.Add(viewNeedle);
            }
        }
    }
}
