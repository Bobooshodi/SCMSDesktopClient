using GalaSoft.MvvmLight;
using SCMSClient.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace SCMSClient.ToastNotification
{
    public sealed class Toaster : ViewModelBase
    {
        #region LazyLoading

        private static Lazy<Toaster> lazy;

        public static Toaster Instance
        {
            get
            {
                if (lazy == null)
                {
                    lazy = new Lazy<Toaster>(() => new Toaster());
                }
                return lazy?.Value;
            }
        }

        private static Notifier _notifier;

        private Toaster()
        {
            _notifier = CreateNotifier(Corner.BottomRight, PositionProviderType.Window, NotificationLifetimeType.TimeBased);
            Application.Current.MainWindow.Closing += MainWindowOnClosing;
        }

        #endregion LazyLoading

        #region notifier configuration

        public static Notifier CreateNotifier(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier?.Dispose();
            _notifier = null;

            return new Notifier(cfg =>
            {
                cfg.PositionProvider = CreatePositionProvider(corner, relation);
                cfg.LifetimeSupervisor = CreateLifetimeSupervisor(lifetime);
                cfg.Dispatcher = Dispatcher.CurrentDispatcher;
            });
        }

        public void ChangePosition(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier = CreateNotifier(corner, relation, lifetime);
        }

        private void MainWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _notifier.Dispose();
        }

        private static INotificationsLifetimeSupervisor CreateLifetimeSupervisor(NotificationLifetimeType lifetime)
        {
            if (lifetime == NotificationLifetimeType.Basic)
                return new CountBasedLifetimeSupervisor(MaximumNotificationCount.FromCount(5));

            return new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(5), MaximumNotificationCount.UnlimitedNotifications());
        }

        private static IPositionProvider CreatePositionProvider(Corner corner, PositionProviderType relation)
        {
            switch (relation)
            {
                case PositionProviderType.Window:
                    {
                        return new WindowPositionProvider(Application.Current.MainWindow, corner, -40, 0);
                    }
                case PositionProviderType.Screen:
                    {
                        return new PrimaryScreenPositionProvider(corner, 5, 5);
                    }
                case PositionProviderType.Control:
                    {
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        var trackingElement = mainWindow;
                        return new ControlPositionProvider(mainWindow, trackingElement, corner, 5, 5);
                    }
            }

            throw new InvalidEnumArgumentException();
        }

        #endregion notifier configuration

        #region notifier messages

        public static string WarningTitle = "Warning";
        public static string SuccessTitle = "Success";
        public static string InformationTitle = "Information";
        public static string ErrorTitle = "Error";

        internal void ShowWarning(string message)
        {
            _notifier.ShowWarning(message, createOptions());
        }

        private static MessageOptions createOptions()
        {
            return new MessageOptions() { FreezeOnMouseEnter = true, ShowCloseButton = true, UnfreezeOnMouseLeave = true };
        }

        internal void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message, createOptions());
        }

        public void ShowInformation(string message)
        {
            _notifier.ShowInformation(message, createOptions());
        }

        public void ShowError(string message)
        {
            _notifier.ShowError(message, createOptions());
        }

        public void ShowCustomizedMessage(string message)
        {
            var options = new MessageOptions
            {
                FontSize = 25,
                ShowCloseButton = false,
                FreezeOnMouseEnter = false,
                NotificationClickAction = n =>
                {
                    n.Close();
                    _notifier.ShowSuccess("clicked!");
                }
            };

            _notifier.ShowError(message, options);
        }

        public void ShowSuccessToast(string title, string message)
        {
            CustomMessageExtensions.ShowSuccessToast(_notifier, createOptions(), title, message, closeAction: c => c.Close());
        }

        public void ShowErrorToast(string title, string message)
        {
            CustomMessageExtensions.ShowErrorToast(_notifier, createOptions(), title, message, closeAction: c => c.Close());
        }

        public void ShowWarningToast(string title, string message)
        {
            CustomMessageExtensions.ShowWarningToast(_notifier, createOptions(), title, message, closeAction: c => c.Close());
        }

        public void ShowInformationToast(string title, string message)
        {
            CustomMessageExtensions.ShowInformationToast(_notifier, createOptions(), title, message, closeAction: c => c.Close());
        }

        #endregion notifier messages

        #region example settings

        private Corner _corner;

        public Corner Corner
        {
            get { return _corner; }
            set
            {
                _corner = value;
                RaisePropertyChanged(() => Corner);
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private PositionProviderType _positionProviderType;

        public PositionProviderType PositionProviderType
        {
            get { return _positionProviderType; }
            set
            {
                _positionProviderType = value;
                RaisePropertyChanged(() => PositionProviderType);
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private NotificationLifetimeType _lifetime;

        public NotificationLifetimeType Lifetime
        {
            get
            {
                return _lifetime;
            }
            set
            {
                _lifetime = value;
                RaisePropertyChanged(() => Lifetime);
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        public bool? FreezeOnMouseEnter { get; set; } = true;
        public bool? ShowCloseButton { get; set; } = false;

        #endregion example settings

        #region Public Methods

        public static void Refresh()
        {
            lazy = null;

            lazy = new Lazy<Toaster>(() => new Toaster());
        }

        #endregion Public Methods
    }
}