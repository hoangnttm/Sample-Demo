using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo.Shared
{
    public class SideSheetViewModelBase : ViewModelBase, ISideSheetViewModel
    {
        public CloseModel<object[]> OnDone { get; set; }
       
        public void Done(params object[] options)
        {
            OnDone?.Invoke(options);
        }

      
    }
}
