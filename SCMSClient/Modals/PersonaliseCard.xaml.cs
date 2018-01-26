using SCMSClient.Models;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for PersonaliseCard.xaml
    /// </summary>
    public partial class PersonaliseCard : UserControl
    {
        public PersonaliseCard(SOAPersonalizationRequest selectedRequest)
        {
            InitializeComponent();

            DataContext = new CardPersonalizationVM(selectedRequest);
        }
    }
}
