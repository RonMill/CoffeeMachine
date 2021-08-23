using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DatabaseService;
using KaffeemaschineWPF.Const;
using KaffeemaschineWPF.Extensions;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.Views;
using Prism.Regions;

namespace KaffeemaschineWPF.ViewModels
{
    public class LoginUserControlViewModel : ObservableObject
    {
        private string _username;
        private string _password;
        private bool _isLoginFailed;
        private readonly IRegionManager _regionManager;
        private PasswordBox _passwordBox;


        private readonly DatabaseManager databaseManager = new DatabaseManager();

        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand PasswordChangedCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

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
        public bool IsLoginFailed
        {
            get => _isLoginFailed;
            set => SetProperty(ref _isLoginFailed, value);
        }
        public LoginUserControlViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            PasswordChangedCommand = new RelayCommandGen<RoutedEventArgs>(PasswordChanged);
            SignInCommand = new RelayCommand(CheckLogin, CheckNotEmpty);
            SignUpCommand = new RelayCommand(OpenRegister);
        }
        private bool CheckNotEmpty()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(Regions.CONTENT_REGION, navigatePath);
        }
        private void CheckLogin()
        {
            if (databaseManager.Login(new User(Username, Password.HashPassword())))
            {
                IsLoginFailed = false;
                //MainWindow mainWindow = new MainWindow();
                //mainWindow.Show();
                Navigate(nameof(CoffeemachineUserControl));
            }
            else
            {
                IsLoginFailed = true;
            }
            ClearFields();
        }
        private void OpenRegister()
        {
            ClearFields();
            Navigate(nameof(SignUpUserControl));
        }
        private void PasswordChanged(RoutedEventArgs args)
        {
            if (args.Source is PasswordBox passwordBox)
            {
                Password = passwordBox.Password;
                if (_passwordBox == null)
                    _passwordBox = passwordBox;
            }
        }
        private void ClearFields()
        {
            _passwordBox.Clear();
            Username = string.Empty;
        }
    }

}