using KaffeemaschineWPF.Const;
using KaffeemaschineWPF.Framework;
using KaffeemaschineWPF.Views;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaffeemaschineWPF.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly IContainerExtension _container;
        private readonly IRegionManager _regionManager;
        public ICommand MainWindowLoadedCommand { get; }
        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
            MainWindowLoadedCommand = new RelayCommand(MainWindow_Loaded);
        }
        private void MainWindow_Loaded()
        {
            IRegion region = _regionManager.Regions[Regions.CONTENT_REGION];
            LoginUserControl loginUserControl = _container.Resolve<LoginUserControl>();
            _ = region.Add(loginUserControl);
        }
    }
}