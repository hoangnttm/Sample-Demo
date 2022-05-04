using AppDemo.Shared;
using AppDemo.ViewModels;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo
{
    public static class DI
    {
        public static ISidesheetService SidesheetService => ContainerLocator.Container.Resolve<ISidesheetService>();
        public static AppSideSheetViewModel SidesheetViewModel => ContainerLocator.Container.Resolve<AppSideSheetViewModel>();
    }
}
