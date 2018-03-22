using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SCMSClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SCMSClient.ViewModel
{
    public class SystemOperatorsVM : ViewModelBase
    {
        #region Private Members

        private ObservableCollection<User> operators;

        #endregion Private Members

        #region Default Constructor

        public SystemOperatorsVM()
        {
            CreateCommand = new RelayCommand(CreateOperator);
            RemoveCommand = new RelayCommand(DeleteOperator);
        }

        #endregion Default Constructor

        #region ICommand Properties

        /// <summary>
        /// <see cref="ICommand"/> to Create a new Operator
        /// </summary>
        public ICommand CreateCommand { get; set; }

        /// <summary>
        /// <see cref="ICommand"/> to Delete an Operator
        /// </summary>
        public ICommand RemoveCommand { get; set; }

        #endregion ICommand Properties

        #region Public Properties

        public ObservableCollection<User> Operators
        {
            get => operators;
            set => Set(ref operators, value, true);
        }

        #endregion Public Properties

        #region Command Methods

        private void CreateOperator()
        {
            throw new NotImplementedException();
        }

        private void DeleteOperator()
        {
            throw new NotImplementedException();
        }

        #endregion Command Methods
    }
}