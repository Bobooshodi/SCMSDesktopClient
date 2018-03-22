using GalaSoft.MvvmLight.Ioc;
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

            var dc = SimpleIoc.Default.GetInstance<ViewModel.CardDistributionVM>();
            dc.SelectedItem = selectedRequest;

            DataContext = dc;
        }
    }
}