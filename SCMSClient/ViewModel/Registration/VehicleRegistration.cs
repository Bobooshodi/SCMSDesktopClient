using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class VehicleRegistrationVM : BaseRegistrationVM
    {
        public VehicleRegistrationVM()
        {
            AddVehicleCommand = new RelayCommand(AddVehicle);

            LoadAll().ConfigureAwait(false);
        }

        public ICommand AddVehicleCommand { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        private async Task LoadAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    Vehicles = RandomDataGenerator.Vehicles(2);
                });
            }
            catch (Exception ex)
            {
                toaster.ShowErrorToast(Toaster.ErrorTitle, ex.Message);
            }
        }

        private void AddVehicle()
        {
            var modal = new Modals.AddVehicle();
            MessengerInstance.Send<UIElement>(modal);
        }

        protected override void ProcessAction()
        {
            Cleanup();

            MainWindowVM.ActivePage = new Uri("/Views/CardholderList.xaml", UriKind.RelativeOrAbsolute);
        }

        protected override bool ValidateFields()
        {
            throw new NotImplementedException();
        }
    }
}