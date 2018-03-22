using GalaSoft.MvvmLight.Ioc;
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
        public PersonaliseCard(SOAPersonalizationRequest selectedRequest, bool isSupplementary = false)
        {
            InitializeComponent();

            var dc = SimpleIoc.Default.GetInstance<CardPersonalizationVM>();
            dc.SelectedItem = selectedRequest;
            dc.PageHeader = isSupplementary ? "Supplement Card" : "Personalise Card";

            DataContext = dc;
        }
    }
}