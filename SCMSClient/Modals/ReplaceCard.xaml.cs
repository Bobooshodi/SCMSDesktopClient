using GalaSoft.MvvmLight.Ioc;
using System.Windows.Controls;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for ReplaceCard.xaml
    /// </summary>
    public partial class ReplaceCard : UserControl
    {
        public ReplaceCard(Models.SOAReplaceCardRequest selectedRequest)
        {
            InitializeComponent();

            var dc = SimpleIoc.Default.GetInstance<ViewModel.CardReplacementVM>();
            dc.SelectedItem = selectedRequest;

            DataContext = dc;
        }
    }
}