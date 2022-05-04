using System;
using System.Threading.Tasks;

namespace AppDemo.Services
{
    public delegate void CloseModel<in T>(T arg);
    public interface IModalService
    {
        void Show(Type type, CloseModel<object[]> ondone = null, params object[] args);

        void Show(string type, CloseModel<object[]> ondone = null, params object[] args);

        Task ShowConfirmDailog(string message, CloseModel<object[]> confirmCallback = null, CloseModel<object[]> cancelCallback = null, bool isShowButtonCancel = true);

        Task ShowDialog(string type, CloseModel<object[]> confirmCallback = null, CloseModel<object[]> cancelCallback = null, params object[] args);

    }
}
