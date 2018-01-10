using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    /// <summary>
    /// Interaction logic for ToasterError.xaml
    /// </summary>
    public partial class ToasterError : NotificationDisplayPart
    {
        public ToasterError()
        {
            InitializeComponent();
        }

        private ErrorNotification _notification;

        public ToasterError(ErrorNotification notification)
        {
            _notification = notification;
            DataContext = notification;
            InitializeComponent();
        }

        public override MessageOptions GetOptions()
        {
            return _notification.Options;
        }
    }
}