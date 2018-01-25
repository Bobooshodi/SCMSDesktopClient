using SCMSClient.Models;
using SCMSClient.ViewModel;
using System.Windows.Controls;

namespace SCMSClient.Modals
{
    /// <summary>
    /// Interaction logic for CardRegistration.xaml
    /// </summary>
    public partial class CardRegistration : UserControl
    {
        public CardRegistration(Card selectedCard)
        {
            InitializeComponent();

            DataContext = new CardRegistrationVM(selectedCard);
        }
    }
}
