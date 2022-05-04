using AppDemo.Shared;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo.ViewModels
{
    public class AppSideSheetViewModel : ViewModelBase
    {
        public IViewModel CurrentViewModel => DI.SidesheetService.CurrentViewModel;
        public bool IsShow => DI.SidesheetService.IsShow;
        public DelegateCommand SidesheetCloseCommand { get; set; }

        public AppSideSheetViewModel()
        {
            SidesheetCloseCommand = new DelegateCommand(() =>
            {
                DI.SidesheetService.Clear();
            });
            DI.SidesheetService.StateChanged += () =>
            {
                RaisePropertyChanged(nameof(CurrentViewModel));
            };
            DI.SidesheetService.PanalStatusChange += () =>
            {
                RaisePropertyChanged(nameof(IsShow));
            };
        }
    }
}
