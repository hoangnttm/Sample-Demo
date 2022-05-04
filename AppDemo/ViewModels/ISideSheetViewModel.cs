using AppDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDemo.ViewModels
{
    public interface ISideSheetViewModel: IViewModel
    {
        CloseModel<object[]> OnDone { get; set; }
    }
}
