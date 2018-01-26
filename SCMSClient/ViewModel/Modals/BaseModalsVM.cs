using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class BaseModalsVM<T> : ViewModelBase
    {
        private T selectedItem;


        public BaseModalsVM(T _selectedItem)
        {
            SelectedItem = _selectedItem;

            ProcessCommand = new RelayCommand(Process);
            CloseCommand = new RelayCommand(CloseModal);
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ProcessCommand { get; set; }


        public T SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value, true);
        }


        protected abstract void Process();

        protected virtual void CloseModal()
        {
            MessengerInstance.Send<UIElement>(null);
        }
    }
}
