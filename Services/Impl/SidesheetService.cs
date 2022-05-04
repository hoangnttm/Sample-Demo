using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Common;
using System.Windows;
using Prism.Mvvm;
using AppDemo.Shared;

namespace AppDemo.Services.Impl
{
    public class SidesheetService : ISidesheetService
    {
        readonly string _sideSheetRegionName = "SideSheetRegion";

        private readonly IList<ISideSheetViewModel> _histories;
        private readonly IRegionManager _regionManager;
       // private readonly IEnumerable<RegisterViewModel> _viewModels;
        private readonly IRegionNavigationService _regionNavigationService;
        private IContainerProvider _containerProvider { get; }

        public SidesheetService(IRegionManager regionManager, 
            IRegionNavigationService regionNavigationService, 
            IContainerProvider containerProvider)
        {
            //_histories = new List<ISidesheetsViewModel>();
            _regionManager = regionManager;
            _regionNavigationService = regionNavigationService;
            _containerProvider = containerProvider;
            //_viewModels = viewModels
        }


        private ISideSheetViewModel _currentViewModel;
        public ISideSheetViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        private bool _show;
        public bool IsShow
        {
            get => _show;
            set
            {
                _show = value;
                PanalStatusChange?.Invoke();
            }
        }

        public int PageCount => _histories.Count;

        public event Action StateChanged;
        public event Action PanalStatusChange;

        public void Clear()
        {
            _regionManager.Regions[_sideSheetRegionName].RemoveAll();
            Pop();
        }

        public void Pop()
        {
            var last = _regionManager.Regions[_sideSheetRegionName].ActiveViews.LastOrDefault();
            if (last is not null)
            {
                _regionManager.Regions[_sideSheetRegionName].Remove(last);
            }

            if (_regionManager.Regions[_sideSheetRegionName].Views.Count() > 0)
            {
                CurrentViewModel = ((FrameworkElement)_regionManager.Regions[_sideSheetRegionName].ActiveViews.LastOrDefault()).DataContext as ISideSheetViewModel;
            }
            else
            {
                IsShow = false;
                CurrentViewModel = null;
            }
        }
        private void ConfigureSideSheet(string sideSheetName,params object[] options)
        {
            var content = _containerProvider.Resolve<object>(sideSheetName);
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A side sheeet's content must be a FrameworkElement");

            ViewModelLocator.SetAutoWireViewModel(dialogContent,true);

            if (!(dialogContent.DataContext is ISideSheetViewModel viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");


            MvvmHelpers.ViewAndViewModelAction<ISideSheetViewModel>(viewModel, d => d.Activity(options));

            _histories.Add(viewModel);
        }
        protected virtual FrameworkElement CreateInstance(Type type)
        {
            var content = _containerProvider.Resolve(type);

            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A side sheeet's content must be a FrameworkElement");

            ViewModelLocator.SetAutoWireViewModel(dialogContent, true);

            return dialogContent;
        }

        public void Push(Type type, params object[] options)
        {
            //if (_histories.Any(x => x.GetType() == type))
            //    return;

            var viewInstance = CreateInstance(type);

            if (!(viewInstance.DataContext is ISideSheetViewModel viewModel))
                throw new NullReferenceException("A side sheeet's ViewModel must implement the ISideSheetViewModel interface");

            _regionManager.AddToRegion(_sideSheetRegionName, viewInstance);
            CurrentViewModel = viewModel;
            if (_regionManager.Regions[_sideSheetRegionName].Views.Count() > 0)
            {
                _regionManager.Regions[_sideSheetRegionName].Activate(viewInstance);
                IsShow = true;
                CurrentViewModel.Activity(options);
            }
        }
        public void Push(Type type, CloseModel<object[]> onDone = null, params object[] options)
        {
            //if (_histories.Any(x => x.GetType() == type))
            //    return;

            var viewInstance = CreateInstance(type);

            if (!(viewInstance.DataContext is ISideSheetViewModel viewModel))
                throw new NullReferenceException("A side sheeet's ViewModel must implement the ISideSheetViewModel interface");

            _regionManager.AddToRegion(_sideSheetRegionName, viewInstance);
            CurrentViewModel = viewModel;
            CurrentViewModel.OnDone = onDone;
            if (_regionManager.Regions[_sideSheetRegionName].Views.Count() > 0)
            {
                _regionManager.Regions[_sideSheetRegionName].Activate(viewInstance);
                IsShow = true;
                CurrentViewModel.Activity(options);
            }
        }
    }
}
