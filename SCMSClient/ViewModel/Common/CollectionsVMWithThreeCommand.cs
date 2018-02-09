using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public abstract class CollectionsVMWithThreeCommand<T> : CollectionsVMWithOneCommand<T>
    {

        private ObservableCollection<T> filteredCollection;

        protected CollectionsVMWithThreeCommand()
        {
            ViewObjectCommand = new RelayCommand(ViewObject);
            FilterCollectionsCommand = new RelayCommand<object>(FilterCollections);
        }

        public ICommand ViewObjectCommand { get; set; }
        public ICommand FilterCollectionsCommand { get; set; }

        protected abstract override Task LoadAll();

        protected abstract override bool SearchFilter(object obj);


        public ObservableCollection<T> FilteredCollection
        {
            get
            {
                if (filteredCollection != null)
                {
                    AllObjectsCollection = CollectionViewSource.GetDefaultView(filteredCollection);
                    AllObjectsCollection.Filter = SearchFilter;
                }

                return filteredCollection;
            }
            set => Set(ref filteredCollection, value, true);
        }


        protected abstract void FilterCollections(object obj);
        protected abstract override void Process();
        protected abstract void ViewObject();
    }
}
