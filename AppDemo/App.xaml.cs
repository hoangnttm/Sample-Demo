using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using System;
using System.Windows;
using AppDemo.Shared;
using AppDemo.Services.Impl;
using AppDemo.ViewModels;

namespace AppDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISidesheetService, SidesheetService>();
            containerRegistry.RegisterSingleton<AppSideSheetViewModel>();
            
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Modules.Core.CoreModule>();
            moduleCatalog.AddModule<Modules.ModuleA.ModuleAModule>();
        }
    }
}
