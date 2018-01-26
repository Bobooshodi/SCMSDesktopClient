using System.Windows.Controls;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for DistributeCards.xaml
    /// </summary>
    public partial class DistributeCards : UserControl
    {
        public DistributeCards(Models.SOACardRequest selectedRequest)
        {
            InitializeComponent();

            DataContext = new ViewModel.CardDistributionVM(selectedRequest);
        }
    }
}
