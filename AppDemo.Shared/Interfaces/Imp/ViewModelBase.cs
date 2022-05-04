using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo.Shared
{
    public class ViewModelBase : BindableBase, IViewModel
    {
        public bool ShouldAnimateOut { get; set; }
        public IViewModel Parent { get; set; }

        public event Action Onloaded;


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
