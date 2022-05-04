using System;
using System.ComponentModel;

namespace AppDemo.ViewModels
{
    public delegate void CloseModel<in T>(T arg);

    public interface IViewModel : INotifyPropertyChanged
    {
        public bool ShouldAnimateOut { get; set; }
        public IViewModel Parent { get; set; }
        abstract void Activity(params object[] options);

        event Action Onloaded;
        void Loaded();

        abstract void Ondestroy();
        abstract void Dispose();

    }
}
