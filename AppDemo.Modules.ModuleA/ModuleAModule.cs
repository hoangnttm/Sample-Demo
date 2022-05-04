using AppDemo.Modules.ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace AppDemo.Modules.ModuleA
{
    public class ModuleAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(AnyView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AnyView>();
        }
    }
}
