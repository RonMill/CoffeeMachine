using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Models;
using KaffeemaschineWPF.States;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaffeemaschineWPF.ViewModels
{
    public class FillBalancelViewModel : ObservableObject, IDialogAware
    {
        private double _refillBalanceAmount;
        private readonly IUserStates _userStates;

        public event Action<IDialogResult> RequestClose;

        public Cashout Kasse { get; }
        public ICommand RefillBalanceCommand { get; }

        public double RefillBalanceAmount
        {
            get => _refillBalanceAmount;
            set => SetProperty(ref _refillBalanceAmount, value);
        }

        public string Title => "Guthaben aufladen";

        public FillBalancelViewModel(IUserStates userStates)
        {
            _userStates = userStates;
            RefillBalanceCommand = new RelayCommand(RefillBalance);
            Kasse = new Cashout();
        }
        private void RefillBalance()
        {
            Kasse.ChangeBalance(_userStates.User, Convert.ToDouble(_userStates.User.Balance) + RefillBalanceAmount);
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}