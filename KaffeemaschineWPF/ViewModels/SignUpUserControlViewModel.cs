using DatabaseService;
using KaffeemaschineWPF.Const;
using KaffeemaschineWPF.Extensions;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.Views;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KaffeemaschineWPF.ViewModels
{
    public class SignUpUserControlViewModel : ObservableObject
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _streetAndHouseNumber;
        private string _postcode;
        private string _city;
        private string _password;
        private string _username;
        private string _balance;
        private bool _isRegisterSuccssed;

        private readonly DatabaseManager databaseManager = new DatabaseManager();
        private readonly IRegionManager _regionManager;

        public ICommand SignUpCommand { get; }
        public ICommand PasswordChangedCommand { get; }
        public ICommand BackToLoginCommand { get; }

        //Properties
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string StreetAndHouseNumber
        {
            get => _streetAndHouseNumber;
            set => SetProperty(ref _streetAndHouseNumber, value);
        }
        public string Postcode
        {
            get => _postcode;
            set => SetProperty(ref _postcode, value);
        }
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }
        public bool IsRegisterSuccssed
        {
            get { return _isRegisterSuccssed; }
            set { SetProperty(ref _isRegisterSuccssed, value); }
        }
        //Ctor
        public SignUpUserControlViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SignUpCommand = new RelayCommand(AddUserMethod, CheckNotEmpty);
            BackToLoginCommand = new RelayCommand(GoToLogin);
            PasswordChangedCommand = new RelayCommandGen<RoutedEventArgs>(PasswordChanged);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(Regions.CONTENT_REGION, navigatePath);
        }
        private void GoToLogin()
        {
            Navigate(nameof(LoginUserControl));
        }

        ~SignUpUserControlViewModel()
        {
            databaseManager.Dispose();
        }

        //Methods
        private void AddUserMethod()
        {
            (string street, int housenumber) = SplitStreetHousenumber();
            databaseManager.AddUser(new User(FirstName, LastName, Email, street, housenumber, Postcode, City, Username, Password.HashPassword(), Balance));
            MessageBox.Show("Registrierung erfolgreich", "Registrierung erfolgreich", MessageBoxButton.OKCancel, MessageBoxImage.Information);

        }
        private (string street, int housenumber) SplitStreetHousenumber()
        {
            string[] StreetHousen = _streetAndHouseNumber.Split(' ');
            return (StreetHousen[0], int.Parse(StreetHousen[1]));
        }
        private bool CheckNotEmpty()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(StreetAndHouseNumber)
                && !string.IsNullOrEmpty(Postcode) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Balance);
        }

        private void PasswordChanged(RoutedEventArgs args)
        {
            if (args.Source is PasswordBox passwordBox)
                Password = passwordBox.Password;
        }
    }
}