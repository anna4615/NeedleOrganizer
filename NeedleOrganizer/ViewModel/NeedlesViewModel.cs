using CommunityToolkit.Mvvm.Input;
using NeedleOrganizer.Interfaces;
using NeedleOrganizer.Models;
using NeedleOrganizer.View;
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

            //_needleService.DeleteAppDataNeedlesFile();
            //await Utils.CreateTestNeedles();

            try
            {
                NeedlesFromDataStorage = await _needleService.GetNeedles();
                PopulateSelectedNeedles(NeedlesFromDataStorage);               
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


        [RelayCommand]
        async Task GoToDetails(ViewNeedle needle)
        {
            if (needle is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"ViewNeedle", needle }
                });
        }


        [RelayCommand]
        async Task GoToAdmin()
        {
            await Shell.Current.GoToAsync(nameof(AdminPage));
        }


        [RelayCommand]
        async Task AddNeedle()
        {
            //TODO: collect new needle from app UI, then remove this hardcoaded one
            Needle needle = new Needle
            {
                //Id = 6,
                Type = "Rundsticka",
                Size = 7,
                IsAvailable = true,
                OnProject = "De här stickorna är lediga."
            };

            try
            {
                await _needleService.AddNeedle(needle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Kunde inte lägga till stickor: {ex.Message}", "OK");
            }
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
