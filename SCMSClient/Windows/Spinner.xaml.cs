using System;
using System.Windows;

namespace SCMSClient.Windows
{
    /// <summary>
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : Window
    {
        private static Lazy<Spinner> lazy = new Lazy<Spinner>(() => new Spinner());

        public static Spinner Instance => lazy?.Value;

        private Spinner()
        {
            InitializeComponent();
        }
    }
}
