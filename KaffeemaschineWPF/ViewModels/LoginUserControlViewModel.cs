using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DatabaseService;
using KaffeemaschineWPF.Extensions;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.Views;

namespace KaffeemaschineWPF.ViewModels
{
    public class LoginUserControlViewModel : ObservableObject
    {
        private string _username;
        private string _password;
        private bool _isLoginFailed;

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
        public LoginUserControlViewModel()
        {
            PasswordChangedCommand = new RelayCommandGen<RoutedEventArgs>(PasswordChanged);
            SignInCommand = new RelayCommand(CheckLogin);
            SignUpCommand = new RelayCommand(OpenRegister);
        }
        private void CheckLogin()
        {
            if (databaseManager.Login(new User(Username, Password.HashPassword())))
            {
                IsLoginFailed = false;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                //MessageBox.Show("True", "True", MessageBoxButton.OK);
            }
            else
            {
                IsLoginFailed = true;

            }
        }
        private void OpenRegister()
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
        }
        private void PasswordChanged(RoutedEventArgs args)
        {
            if (args.Source is PasswordBox passwordBox)
                Password = passwordBox.Password;
        }
    }

}