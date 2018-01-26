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

            DataContext = new ViewModel.CardReplacementVM(selectedRequest);
        }
    }
}
