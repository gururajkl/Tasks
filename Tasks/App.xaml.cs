using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Tasks.ViewModels;
using Tasks.Views;

namespace Tasks
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<Detail, DetailViewModel>();
        }
    }
}
