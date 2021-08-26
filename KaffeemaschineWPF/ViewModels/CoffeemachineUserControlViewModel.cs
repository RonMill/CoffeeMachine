﻿using DatabaseService;
using KaffeemaschineWPF.Const;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.States;
using KaffeemaschineWPF.Views;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace KaffeemaschineWPF.ViewModels
{
    public class CoffeemachineUserControlViewModel : ObservableObject
    {
        private double _fillWaterAmount;
        private double _fillBeansAmount;
        private double _makeCoffeeAmount;
        private double _refillBalanceAmount;
        private bool _isCoffeeVisible;
        private bool _isMakingCoffee;
        private bool _isRefilling;
        private string _username;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _balance;
        private CoffeeBrandEnum _coffeeBrand;
        private readonly IUserStates _userStates;
        private readonly IRegionManager _regionManager;
        private readonly DatabaseManager databaseManager = new DatabaseManager();
        private readonly IDialogService _dialogService;
        public ICommand FillWaterCommand { get; }
        public ICommand FillBeansCommand { get; }
        public ICommand MakeCoffeeCommand { get; }
        public ICommand BackToLoginCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand RefillBalanceCommand { get; }
        public Cashout Kasse { get; }
        public CoffeeMachine KaffeeMaschine { get; }
        public bool IsCoffeeVisible
        {
            get => _isCoffeeVisible;
            set => SetProperty(ref _isCoffeeVisible, value);
        }
        public double FillWaterAmount
        {
            get => _fillWaterAmount;
            set => SetProperty(ref _fillWaterAmount, value);
        }
        public double FillBeansAmount
        {
            get => _fillBeansAmount;
            set => SetProperty(ref _fillBeansAmount, value);
        }
        public double MakeCoffeeAmount
        {
            get => _makeCoffeeAmount;
            set
            {
                SetProperty(ref _makeCoffeeAmount, value);
                RefreshPrice();
            }
        }
        public double RefillBalanceAmount
        {
            get => _refillBalanceAmount;
            set => SetProperty(ref _refillBalanceAmount, value);
        }
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string Firstname
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }
        public string Lastname
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }
        public CoffeeStrengthEnum SelectedCoffeeStrength { get; set; }
        public CoffeeBrandEnum SelectedCoffeeBrand
        {
            get => _coffeeBrand;
            set
            {
                SetProperty(ref _coffeeBrand, value);
                RefreshPrice();
            }
        }
        public CoffeemachineUserControlViewModel(IRegionManager regionManager, IUserStates userStates, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _userStates = userStates;
            _dialogService = dialogService;
            KaffeeMaschine = new CoffeeMachine();
            Kasse = new Cashout();
            FillWaterCommand = new RelayCommand(FillWaterMethod, () => FillWaterAmount > 0 && !_isRefilling);
            FillBeansCommand = new RelayCommand(FillBeansMethod, () => FillBeansAmount > 0 && !_isRefilling);
            MakeCoffeeCommand = new RelayCommand(MakeCoffeeAndShowMessage, () => MakeCoffeeAmount > 0 && !_isMakingCoffee);
            BackToLoginCommand = new RelayCommand(GoToLogin);
            RefreshCommand = new RelayCommand(RefreshPrice);
            RefillBalanceCommand = new RelayCommand(RefillBalance);
            SetUserInformations();
        }

        private void RefillBalance()
        {
            _dialogService.ShowDialog(nameof(FillBalanceView));
            SetUserInformations();
        }

        private void SetUserInformations()
        {
            Username = _userStates.User.Username;
            Firstname = _userStates.User.FirstName;
            Lastname = _userStates.User.LastName;
            Email = _userStates.User.Email;
            Balance = _userStates.User.Balance;
        }
        private async void RefreshPrice()
        {
            await Kasse.GetPriceToPay(SelectedCoffeeBrand, MakeCoffeeAmount);
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
        private async void MakeCoffeeAndShowMessage()
        {
            double newBalance = Math.Round(Convert.ToDouble(_userStates.User.Balance) - Convert.ToDouble(Kasse.PriceToPay), 2);
            if (newBalance < 0)
            {
                MessageBox.Show("Nicht genügen Guthaben.\nBitte Guthaben aufladen");
                return;
            }
            var text = KaffeeMaschine.Calculate(MakeCoffeeAmount, SelectedCoffeeStrength);
            if (text != CoffeeMessageEnum.Ok)
            {
                ShowMessage(text);
                return;
            }
            _isMakingCoffee = true;
            await KaffeeMaschine.PrepareCoffee();
            IsCoffeeVisible = true;
            await KaffeeMaschine.MakeCoffeeSound();
            IsCoffeeVisible = false;
            _isMakingCoffee = false;
            Kasse.ChangeBalance(_userStates.User, newBalance);
            SetUserInformations();
        }
        private async void FillWaterMethod()
        {
            _isRefilling = true;
            await KaffeeMaschine.FillWater(FillWaterAmount);
            _isRefilling = false;
        }
        private async void FillBeansMethod()
        {
            _isRefilling = true;
            await KaffeeMaschine.FillBeans(FillBeansAmount);
            _isRefilling = false;
        }
        private string GetUserMessage(CoffeeMessageEnum kaffeemeldung)
        {
            switch (kaffeemeldung)
            {
                case CoffeeMessageEnum.WaterLow: return "Wasser aufüllen";
                case CoffeeMessageEnum.BeansLow: return "Bohnen auffüllen";
                case CoffeeMessageEnum.AmountToHigh: return "Maximale Menge überschritten";
                default: return "";
            }
        }
        private void ShowMessage(CoffeeMessageEnum kaffeemeldung)
        {
            if (kaffeemeldung == CoffeeMessageEnum.Ok)
                return;
            string message = GetUserMessage(kaffeemeldung);
            MessageBox.Show(message, "Kaffeemeldung", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}