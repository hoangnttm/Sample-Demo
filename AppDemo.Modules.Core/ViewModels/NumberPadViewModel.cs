using AppDemo.Shared;
using Prism.Commands;

namespace AppDemo.Modules.Core.ViewModels
{
    public class NumberPadViewModel : SideSheetViewModelBase
    {
        private static readonly string _defaultValue = "0";
        private readonly ISidesheetService _sidesheetService;
        private string _title = string.Empty;
        private string _textSelected = _defaultValue;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string TextSelected
        {
            get { return _textSelected; }
            set { SetProperty(ref _textSelected, value); }
        }
        public DelegateCommand<string> SelectNumberCommand { get; private set; }
        public DelegateCommand<object> DeleteCommand { get; private set; }
        public DelegateCommand<string> SubmitCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }

        public NumberPadViewModel(ISidesheetService sidesheetService)
        {
            _sidesheetService = sidesheetService;

            SelectNumberCommand = new DelegateCommand<string>((number) =>
            {
                if (double.TryParse(TextSelected + number, out double newNumber))
                    TextSelected = newNumber.ToString();
                else
                    TextSelected = _defaultValue;
            });
            DeleteCommand = new DelegateCommand<object>((obj) =>
            {
                if (obj is null)
                {
                    if (TextSelected.Equals(_defaultValue)) return;

                    var newText = TextSelected.Remove(TextSelected.Length - 1, 1);
                    if (newText.Length == 0)
                        newText = _defaultValue;

                    TextSelected = newText;
                }
                else
                    TextSelected = "0";
            });
            SubmitCommand = new DelegateCommand<string>((text) => Done(TextSelected));
            CloseCommand = new DelegateCommand(() => _sidesheetService.Clear());
        }
        public override void Activity(params object[] options)
        {
            if (options is not null && options.Length > 0 && options[0] is string)
            {
                TextSelected = options[0] as string;
            }
        }
    }
}
