using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.ToastNotification;
using SCMSClient.Utilities;
using System;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class BaseSettingsVM<T> : ViewModelBase
    {
        protected T mainObject;
        protected readonly Toaster toaster = Toaster.Instance;
        protected readonly IDinkeyDongleService dongleService;
        protected readonly ISettingsService settingsService;
        protected virtual bool CanProcess { get; }

        protected BaseSettingsVM(ISettingsService _settingsService, IDinkeyDongleService _dongleService)
        {
            dongleService = _dongleService;
            settingsService = _settingsService;

            CloseModalCommand = new RelayCommand(CloseModal);
            ProcessCommand = new RelayCommand<object>(Process, (object obj) => CanProcess);

            AppSettings = LoadAppSettings();
            LoadSetting();
        }

        public ICommand CloseModalCommand { get; set; }

        public ICommand ProcessCommand { get; set; }

        public bool IsCalledFromWithinApp { get; set; }

        public ApplicationSettings AppSettings { get; set; }

        public T MainObject
        {
            get => mainObject;

            set => Set(ref mainObject, value, true);
        }

        protected virtual void CloseModal()
        {
            MessengerInstance.Send<UIElement>(null);
        }

        protected virtual void LoadSetting()
        {
        }

        protected ApplicationSettings LoadAppSettings()
        {
            return settingsService.LoadSettings() ?? (AppSettings = new ApplicationSettings());
        }

        protected abstract void Process(object obj);

        protected virtual void Save(ApplicationSettings settings)
        {
            try
            {
                var deleted = settingsService.DeleteSettings();

                var saved = settingsService.SaveSettings(settings);
                Application.Current.Properties["appSettings"] = settings;
                AppSettings = settings;

                MessengerInstance.Send(settings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                ErrorLogger.LogError(ex.ToString(), ErrorType.APPLICATION_ERROR);

                throw;
            }
        }

        protected virtual void BrodacastChanges(ApplicationSettings settings)
        {
            MessengerInstance.Send(settings);
        }
    }
}