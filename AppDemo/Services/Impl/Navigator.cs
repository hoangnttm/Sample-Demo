using Punch.HostMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AppDemo.Services.Impl
{
    public class Navigator : INavigator
    {
        private readonly IEnumerable<RegisterViewModel> _viewModels;
        private bool _isLoadingPage = false;
        public Navigator(IEnumerable<RegisterViewModel> viewModels)
        {
            _viewModels = viewModels;
        }

        private IViewModel _currentViewModel;
        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;

        public void NavigaTo(Type type, params object[] options)
        {
            _ = Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background, new Action(() =>
                {
                    if (this._isLoadingPage) return;
                    DI.SidesheetService.Clear();
                    if (_currentViewModel != null && _currentViewModel.GetType() == type)
                    {
                        return;
                    }

                    if (_currentViewModel != null)
                    {
                        _currentViewModel.Dispose();
                        _currentViewModel = null;
                    }
                    CurrentViewModel = _viewModels.FirstOrDefault(x => x.Type.Name == type.Name).CreateViewModel();

                    CurrentViewModel.Onloaded += () =>
                    {
                        this._isLoadingPage = false;
                    };
                    this._isLoadingPage = true;
                    CurrentViewModel.Activity(options);
                }));
        }
    }
}
