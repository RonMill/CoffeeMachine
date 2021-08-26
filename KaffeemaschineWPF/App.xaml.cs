﻿using KaffeemaschineWPF.States;
using KaffeemaschineWPF.ViewModels;
using KaffeemaschineWPF.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KaffeemaschineWPF {
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SignUpUserControl>();
            containerRegistry.RegisterForNavigation<LoginUserControl>();
            containerRegistry.RegisterForNavigation<CoffeemachineUserControl>();
            containerRegistry.RegisterSingleton<IUserStates,UserStates>();
            containerRegistry.RegisterDialog<FillBalanceView, FillBalancelViewModel>();
        }
    }
}
