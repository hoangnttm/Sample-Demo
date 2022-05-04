using AppDemo.Modules.Core.Views;
using AppDemo.Shared;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace AppDemo.Modules.ModuleA.ViewModels
{
    public class AnyViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ISidesheetService _sidesheetService;
        private bool _isShowPopup;
        private string _text;

        public bool IsShowPopup
        {
            get { return _isShowPopup; }
            set { SetProperty(ref _isShowPopup, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }


        public DelegateCommand VisibilityPopupCommand { get; private set; }
        public DelegateCommand<string> EnterEditCommand { get; private set; }

        public AnyViewModel(IRegionManager regionManager, ISidesheetService sidesheetService)
        {
            EnterEditCommand = new DelegateCommand<string>(OpenNumberPad);
            VisibilityPopupCommand = new DelegateCommand(ShowPopup);
            _regionManager = regionManager;
            _sidesheetService = sidesheetService;
        }
        private void ShowPopup()
        {
            IsShowPopup = !IsShowPopup;
        }
        private void OpenNumberPad(string number)
        {
            _sidesheetService.Push(typeof(NumberPad), (obj) =>
            {
                if(obj is not null && obj.Length > 0)
                {
                    Text = obj[0] as string;
                }
                _sidesheetService.Pop();
            },Text);
        }
    }
}
