using ToastNotifications.Core;

namespace SCMSClient.ToastNotification.Information
{
    /// <summary>
    /// Interaction logic for Information.xaml
    /// </summary>
    public partial class Information : NotificationDisplayPart
    {
        public Information()
        {
            InitializeComponent();
        }

        private InformationNotification _notification;

        public Information(InformationNotification notification)
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