using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserInfo.Core;
using UserInfo.Data.Interface;
using UserInfo.Data.SqlOperations;
using UserInfo.Views;

namespace UserInfo.ViewsModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        #region Fields
        private IDataHelper<User> _dataHelper;
        private User _user;
        private User _selectedUser;
        private ObservableCollection<User> _users;
        private AddUser _userView;
        #endregion
        #region Properties
        public string FullName
        {
            get { return _user.FullName; }
            set
            {
                if (_user.FullName != value)
                {
                    _user.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public string Email
        {
            get { return _user.Email; }
            set
            {
                if (_user.Email != value)
                {
                    _user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string PhoneNumber
        {
            get { return _user.PhoneNumber; }
            set
            {
                if (_user.PhoneNumber != value)
                {
                    _user.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users; set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }
        public AddUser UserView { get => _userView; set => _userView = value; }
        #endregion
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand RefreshCommand { get; }

        #region Constructor
        public UserViewModel()
        {
            _user = new User();
            _users = new ObservableCollection<User>();
            _dataHelper = new UserEntity();

            LoadData();
            AddCommand = new RelayCommand(AddData);
            EditCommand = new RelayCommand(EditData);
            SaveCommand = new RelayCommand(SaveData);
            DeleteCommand = new RelayCommand(DeleteData);
            RefreshCommand = new RelayCommand(RefreshData);
        }

        private void RefreshData()
        {
            LoadData();
        }

        private async void DeleteData()
        {
            if(SelectedUser != null)
            {
                await _dataHelper.DeleteAsync(SelectedUser.Id);
                LoadData();
            }
            else
            {
                MessageBox.Show("It's necessary select a item to edit.", "Warning", MessageBoxButton.OK);
            }
        }

        private async void SaveData()
        {                        
            if (SelectedUser == null)
            {
                _user = new User()
                {
                    FullName = FullName,
                    Email = Email,
                    PhoneNumber = PhoneNumber
                };
                await _dataHelper.AddAsync(_user);                
                ClearData();
            }
            else
            {
                _user = new User()
                {
                    FullName = FullName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Id = SelectedUser.Id
                };
                await _dataHelper.EditAsync(_user);
            }
            LoadData();
            _userView.Close();
        }

        private void AddData()
        {
            ClearData();
            _selectedUser = null;
            _userView = new AddUser(this);
            _userView.Show();
        }
        private void EditData()
        {
            if(SelectedUser != null)
            {
                SetData();
                _userView = new AddUser(this);
                _userView.Show();
            }
            else
            {
                MessageBox.Show("It's necessary select a item to edit.", "Warning", MessageBoxButton.OK);
            }
        }

        private void SetData()
        {
            FullName = _selectedUser.FullName;
            Email = _selectedUser.Email;
            PhoneNumber = _selectedUser.PhoneNumber;
        }

        private void ClearData()
        {
            FullName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;            
        }

        private async void LoadData()
        {
            _users.Clear();
            foreach (User user in await _dataHelper.GetAllAsync())
            {
                _users.Add(user);
            }
        }
        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
