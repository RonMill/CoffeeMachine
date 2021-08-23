using KaffeemaschineWPF.Const;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.Views;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KaffeemaschineWPF.ViewModels
{
    public class CoffeemachineUserControlViewModel : ObservableObject
    {

        private double _fillWaterAmount=2.5;
        private double _fillBeansAmount=2.5;
        private double _makeCoffeeAmount;
        private bool _isCoffeeVisible;
        private bool _isMakingCoffee;
        private bool _isFilling;
        private readonly IRegionManager _regionManager;

        public ICommand FillWaterCommand { get; }
        public ICommand FillBeansCommand { get; }
        public ICommand MakeCoffeeCommand { get; }
        public ICommand BackToLoginCommand { get; }
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
            set => SetProperty(ref _makeCoffeeAmount, value);
        }

        public CoffeeStrength SelectedCoffeeStrength { get; set; }
        public CoffeemachineUserControlViewModel(IRegionManager regionManager)
        {
            
            _regionManager = regionManager;
            KaffeeMaschine = new CoffeeMachine();
            FillWaterCommand = new RelayCommand(FillWaterMethod, () => FillWaterAmount > 0 && !_isFilling);
            FillBeansCommand = new RelayCommand(FillBeansMethod, () => FillBeansAmount > 0 && !_isFilling);
            MakeCoffeeCommand = new RelayCommand(MakeCoffeeAndShowMessage, () => MakeCoffeeAmount > 0 && !_isMakingCoffee);
            BackToLoginCommand = new RelayCommand(GoToLogin);
            FillWaterMethod();
            FillBeansMethod();
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
            _isMakingCoffee = true;
            await KaffeeMaschine.PrepareCoffee();
            IsCoffeeVisible = true;
            var text = await KaffeeMaschine.MakeCoffee(MakeCoffeeAmount, SelectedCoffeeStrength);
            ShowMessage(text);
            IsCoffeeVisible = false;
            _isMakingCoffee = false;
        }


        private async void FillWaterMethod()
        {
            _isFilling = true;
            await KaffeeMaschine.FillWater(FillWaterAmount);
            _isFilling = false;
        }
        private async void FillBeansMethod()
        {
            _isFilling = true;
            await KaffeeMaschine.FillBeans(FillBeansAmount);
            _isFilling = false;
        }
        private string GetUserMessage(CoffeeMessage kaffeemeldung)
        {
            switch (kaffeemeldung)
            {

                case CoffeeMessage.WaterLow: return "Wasser aufüllen";
                case CoffeeMessage.BeansLow: return "Bohnen auffüllen";
                case CoffeeMessage.AmountToHigh: return "Maximale Menge überschritten";
                default: return "";
            }
        }
        private void ShowMessage(CoffeeMessage kaffeemeldung)
        {
            if (kaffeemeldung == CoffeeMessage.Ok)
                return;
            string message = GetUserMessage(kaffeemeldung);
            MessageBox.Show(message, "Kaffeemeldung", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}