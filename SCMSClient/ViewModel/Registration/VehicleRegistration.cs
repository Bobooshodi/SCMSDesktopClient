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
            RemoveVehicleCommand = new RelayCommand(RemoveVehicle);

            LoadAll().ConfigureAwait(false);
        }

        public ICommand AddVehicleCommand { get; set; }
        public ICommand RemoveVehicleCommand { get; set; }

        protected override bool CanPerformAction => ValidateFields();

        public List<Vehicle> Vehicles { get; set; }
        public Vehicle SelectedVehicle { get; set; }

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

        private void RemoveVehicle()
        {
            Vehicles.Remove(SelectedVehicle);
        }

        protected override void ProcessAction()
        {
            Cleanup();

            MainWindowVM.ActivePage = new Uri("/Views/CardholderList.xaml", UriKind.RelativeOrAbsolute);
        }

        private void CoupleCardholderDetails()
        {
        }

        protected override bool ValidateFields()
        {
            return true;
        }
    }
}