using Prism.Mvvm;
using System;

namespace AppDemo.Services
{
    public interface INavigator
    {
        BindableBase CurrentViewModel { get; set; }

        void NavigaTo(Type type, params object[] options);

        event Action StateChanged;
    }
}
