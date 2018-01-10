using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    /// <summary>
    /// Interaction logic for ToasterSuccess.xaml
    /// </summary>
    public partial class ToasterSuccess : NotificationDisplayPart
    {
        public ToasterSuccess()
        {
            InitializeComponent();
        }

        private SuccessNotification _customNotification;

        public ToasterSuccess(SuccessNotification customNotification)
        {
            _customNotification = customNotification;
            DataContext = customNotification;
            InitializeComponent();
        }

        public override MessageOptions GetOptions()
        {
            return _customNotification.Options;
        }
    }
}