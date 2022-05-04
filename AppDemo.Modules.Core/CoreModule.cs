using AppDemo.Modules.Core.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace AppDemo.Modules.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NumberPad>();
        }
    }
}
