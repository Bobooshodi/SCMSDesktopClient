using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SCMSClient.Spinner
{
    /// <summary>
    /// Interaction logic for LoaderSpinner.xaml
    /// </summary>
    public partial class LoaderSpinner : UserControl
    {
        public LoaderSpinner()
        {
            InitializeComponent();
        }

        public Brush SpinnerColor
        {
            get => (Brush)GetValue(SpinnerColorProperty);
            set => SetValue(SpinnerColorProperty, value);
        }

        public Visibility SpinnerVisibility
        {
            get => (Visibility)GetValue(SpinnerVisibilityProperty);
            set => SetValue(SpinnerVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for SpinnerColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(LoaderSpinner), new PropertyMetadata());

        // Using a DependencyProperty as the backing store for SpinnerVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerVisibilityProperty =
            DependencyProperty.Register("SpinnerVisibility", typeof(Visibility), typeof(LoaderSpinner), new PropertyMetadata(Visibility.Hidden));




    }
}
