using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo.ViewModels
{
    public class SideSheetViewModelBase : BindableBase, ISideSheetViewModel
    {
        public CloseModel<object[]> OnDone { get; set; }
        public bool ShouldAnimateOut { get; set; }
        public IViewModel Parent { get; set; }

        public event Action Onloaded;

        public void Done(params object[] options)
        {
            OnDone?.Invoke(options);
        }

        public virtual void Activity(params object[] options)
        {
            this.Onloaded?.Invoke();
        }

        public virtual void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void Loaded()
        {
            this.Onloaded?.Invoke();
        }

        public virtual void Ondestroy()
        {
        }
    }
}
