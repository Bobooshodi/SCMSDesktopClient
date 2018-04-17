using GalaSoft.MvvmLight.Messaging;
using SCMSClient.Models;
using System;
using System.Windows;

namespace SCMSClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Application.Current.MainWindow = this;
        }
    }
}