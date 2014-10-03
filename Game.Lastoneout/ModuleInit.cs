using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Lastoneout.GameInfrastructure;
using Game.Lastoneout.GameInfrastructure.AiPLayer;
using Game.Lastoneout.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Game.Lastoneout
{
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        //private MainRegionController _mainRegionController;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IGameService, LastoneoutGameService>(new ContainerControlledLifetimeManager());

            var aiPlayerProvider = new AiPlayerProvider();
            aiPlayerProvider.AddPlayer(AiPlayers.Bender, new BenderPlayer());
            aiPlayerProvider.AddPlayer(AiPlayers.R2D2, new R2D2Player());
            aiPlayerProvider.AddPlayer(AiPlayers.Skynet, new SkynetPlayer());
            _container.RegisterInstance(typeof (IAiPlayerProvider), aiPlayerProvider, new ContainerControlledLifetimeManager());

            _regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, () => _container.Resolve<GameView>());
            //_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, () => _container.Resolve<MiddleView>());
            //_regionManager.RegisterViewWithRegion(RegionNames.RightRegion, () => _container.Resolve<RightPlayerView>());

            //// This is an example of View Discovery which associates the specified view type
            //// with a region so that the view will be automatically added to the region when
            //// the region is first displayed.

            //// TODO: 03 - The EmployeeModule configures the EmployeeListView to automatically appear in the Left region (using View Discovery).
            //// Show the Employee List view in the shell's left hand region.
            //this._regionManager.RegisterViewWithRegion(RegionNames.LeftRegion,
            //                                           () => this.container.Resolve<EmployeeListView>());

            //// TODO: 04 - The EmployeeModule defines a controller class, MainRegionController, which programmatically displays views in the Main region (using View Injection).
            //// Create the main region controller.
            //// This is used to programmatically coordinate the view
            //// in the main region of the shell.
            //this._mainRegionController = this.container.Resolve<MainRegionController>();

            //// TODO: 08 - The EmployeeModule configures the EmployeeDetailsView and EmployeeProjectsView to automatically appear in the Tab region (using View Discovery).
            //// Show the Employee Details and Employee Projects view in the tab region.
            //// The tab region is defined as part of the Employee Summary view which is only
            //// displayed once the user has selected an employee in the Employee List view.
            //this._regionManager.RegisterViewWithRegion(RegionNames.TabRegion,
            //                                           () => this.container.Resolve<EmployeeDetailsView>());
            //this._regionManager.RegisterViewWithRegion(RegionNames.TabRegion,
            //                                           () => this.container.Resolve<EmployeeProjectsView>());
        }
    }
}
