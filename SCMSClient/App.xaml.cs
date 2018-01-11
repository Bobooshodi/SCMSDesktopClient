using SCMSClient.Utilities;
using System.Threading;
using System.Windows;

namespace SCMSClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string MutexName = "1d7db30f-74c8-45ef-a835-a7376e733683";

        private readonly Mutex _mutex;

        // overload the constructor
        private bool createdNew;

        public App()
        {
            _mutex = new Mutex(true, MutexName, out createdNew);

            UnityConfig.Initialize();

            if (!createdNew)
            {
                // if the mutex already exists, notify and quit
                MessageBox.Show("This program is already running");
                Current.Shutdown();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!createdNew) return;
            // overload the OnStartup so that the main window
            // is constructed and visible
            var window = new MainWindow
            {
                WindowState = WindowState.Maximized
            };
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Shutdown();
        }
    }
}
