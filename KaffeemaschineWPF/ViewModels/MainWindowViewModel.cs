using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
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
    public class MainWindowViewModel : ObservableObject
    {

        private double _fillWaterAmount;
        private double _fillBeansAmount;
        private double _makeCoffeeAmount;
        private bool _isCoffeeVisible;
        private bool _isMakingCoffee;
        private bool _isFilling;

        public ICommand FillWaterCommand { get; }
        public ICommand FillBeansCommand { get; }
        public ICommand MakeCoffeeCommand { get; }
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
        public MainWindowViewModel()
        {
            KaffeeMaschine = new CoffeeMachine();
            FillWaterCommand = new RelayCommand(FillWaterMethod, () => FillWaterAmount > 0 && !_isFilling);
            FillBeansCommand = new RelayCommand(FillBeansMethod, () => FillBeansAmount > 0 && !_isFilling);
            MakeCoffeeCommand = new RelayCommand(MakeCoffeeAndShowMessage, () => MakeCoffeeAmount > 0 && !_isMakingCoffee);
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