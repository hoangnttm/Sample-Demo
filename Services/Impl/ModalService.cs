using Punch.HostMain.Commands;
using Punch.HostMain.ViewModels;
using Punch.HostMain.ViewModels.Impl.Modals;
using Punch.HostMain.ViewModels.Impl.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AppDemo.Services.Impl
{
    public class ModalService : IModalService
    {
        private readonly IEnumerable<RegisterViewModel> _viewModels;

        public ModalService(IEnumerable<RegisterViewModel> viewModels)
        {
            _viewModels = viewModels;
        }

        public void Show(Type type, CloseModel<object[]> onDone = null, params object[] args)
        {
            Show(type.Name, onDone, args);
        }

        public void Show(string type, CloseModel<object[]> onDone = null, params object[] args)
        {
            var currentViewModel = (IModalViewModel)_viewModels.FirstOrDefault(x => x.Type.Name == type).CreateViewModel();
            currentViewModel.OnOk = onDone;

            _ = Application.Current.Dispatcher.BeginInvoke(
            DispatcherPriority.Background, new Action(() =>
            {
                OnshowPoup(currentViewModel);
            }));

            _ = Application.Current.Dispatcher.BeginInvoke(
             DispatcherPriority.Background, new Action(() =>
             {
                 currentViewModel.Activity(args);
             }));
        }
        public Task ShowDialog(Type type, CloseModel<object[]> confirmCallback = null, CloseModel<object[]> cancelCallback = null, params object[] args)
        {
            return ShowDialog(type.Name, confirmCallback, cancelCallback, args);
        }
        public Task ShowDialog(string type, CloseModel<object[]> confirmCallback = null, CloseModel<object[]> cancelCallback = null, params object[] args)
        {
            var currentViewModel = (IModalViewModel)_viewModels.FirstOrDefault(x => x.Type.Name == type).CreateViewModel();
            currentViewModel.OnOk = confirmCallback;
            currentViewModel.OnCancel = cancelCallback;

            _ = Application.Current.Dispatcher.BeginInvoke(
             DispatcherPriority.Background, new Action(() =>
             {
                 currentViewModel.Activity(args);
             }));

            return OnshowPoup(currentViewModel);

        }

        public Task ShowConfirmDailog(string message, CloseModel<object[]> confirmCallback = null, CloseModel<object[]> cancelCallback = null, bool isShowButtonCancel = true)
        {
            var viewModel = DI.GetInstance<ConfirmDialogViewModel>();
            viewModel.OnOk = confirmCallback;
            viewModel.OnCancel = cancelCallback;
            viewModel.Message = message;
            viewModel.IsShowButtonCancel = isShowButtonCancel;

            return OnshowPoup(viewModel);
        }

        public static Task OnshowPoup(IModalViewModel viewModel)
        {
            var tcs = new TaskCompletionSource<bool>();
            // Run on UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    DialogWindow dialogWindow = new DialogWindow();
                    viewModel.CloseCommand = new RelayCommand(() => dialogWindow.Close());
                    dialogWindow.DataContext = new DialogWindowViewModel(dialogWindow)
                    {
                        WindowMinimumWidth = viewModel.WindowMinimumWidth,
                        WindowMinimumHeight = viewModel.WindowMinimumHeight,
                        TitleHeight = viewModel.TitleHeight,
                        Title = viewModel.Title ?? "",
                        Content = viewModel
                    };

                    dialogWindow.Owner = Application.Current.MainWindow;
                    dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    dialogWindow.ShowDialog();
                    dialogWindow = null;
                }
                finally
                {
                    viewModel.Dispose();
                    tcs.TrySetResult(true);
                }
            });

            return tcs.Task;
        }
    }
}
