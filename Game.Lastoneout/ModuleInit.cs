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
        }
    }
}
