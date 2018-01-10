using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    /// <summary>
    /// Interaction logic for ToasterWarning.xaml
    /// </summary>
    public partial class ToasterWarning : NotificationDisplayPart
    {
        public ToasterWarning()
        {
            InitializeComponent();
        }

        private WarningNotification _notification;

        public ToasterWarning(WarningNotification notification)
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