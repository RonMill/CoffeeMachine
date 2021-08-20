using DatabaseService;
using KaffeemaschineWPF.Extensions;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
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
    public class SignUpViewModel : ObservableObject
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _streetAndHouseNumber;
        private string _postcode;
        private string _city;
        private string _password;
        private string _username;
        private bool _isRegisterSuccssed;

        private readonly DatabaseManager databaseManager = new DatabaseManager();

        public ICommand SignUpCommand { get; }
        public ICommand PasswordChangedCommand { get; }

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
        public bool IsRegisterSuccssed
        {
            get { return _isRegisterSuccssed; }
            set { SetProperty(ref _isRegisterSuccssed, value); }
        }
        //Ctor
        public SignUpViewModel()
        {
            SignUpCommand = new RelayCommand(AddUserMethod, CheckNotEmpty);
            PasswordChangedCommand = new RelayCommandGen<RoutedEventArgs>(PasswordChanged);
        }
        ~SignUpViewModel()
        {
            databaseManager.Dispose();
        }

        //Methods
        private void AddUserMethod()
        {
            (string street, int housenumber) = SplitStreetHousenumber();
            databaseManager.AddUser(new User(FirstName, LastName, Email, street, housenumber, Postcode, City, Username, Password.HashPassword()));
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
                && !string.IsNullOrEmpty(Postcode) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void PasswordChanged(RoutedEventArgs args)
        {
            if(args.Source is PasswordBox passwordBox)
                Password = passwordBox.Password;
        }
    }
}