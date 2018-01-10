using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    public class ErrorNotification : NotificationBase, INotifyPropertyChanged
    {
        private ToasterError _displayPart;
        private Action<ErrorNotification> _closeAction;
        public MessageOptions Options;

        public ICommand CloseCommand { get; set; }

        public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = new ToasterError(this));

        public ErrorNotification(string title, string message, Action<ErrorNotification> closeAction, MessageOptions options)
        {
            Title = title;
            Message = message;
            _closeAction = closeAction;
            Options = options;

            CloseCommand = new ToasterRelayCommand(x => _closeAction(this));
        }

        #region binding properties

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion binding properties
    }
}