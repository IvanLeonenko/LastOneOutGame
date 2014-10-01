using System.Windows;
using Game.Shell.Services;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace Game.Shell
{
    public class GameBootstraper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            Container.RegisterType<IUserInfoService, UserInfoService>(new ContainerControlledLifetimeManager());
            base.ConfigureContainer();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Lastoneout.ModuleInit));
        }

        protected override DependencyObject CreateShell()
        {
            // Using container to create shell instance, container then 
            // used through whole application resolving dependencies for ViewModels and Views
            var view = this.Container.TryResolve<ShellView>();
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
