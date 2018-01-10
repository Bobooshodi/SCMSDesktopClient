﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    public class SuccessNotification : NotificationBase, INotifyPropertyChanged
    {
        private ToasterSuccess _displayPart;
        private Action<SuccessNotification> _closeAction;
        public MessageOptions Options;

        public ICommand CloseCommand { get; set; }

        public override NotificationDisplayPart DisplayPart => _displayPart ?? (_displayPart = new ToasterSuccess(this));

        public SuccessNotification(string title, string message, Action<SuccessNotification> closeAction, MessageOptions options)
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
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion binding properties
    }
}