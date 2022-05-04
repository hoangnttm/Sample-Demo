using System;

namespace AppDemo.Shared
{
    public interface ISidesheetService
    {
        ISideSheetViewModel CurrentViewModel { get; set; }

        int PageCount { get; }

        bool IsShow { get; set; }

        void Push(Type type, params object[] options);

        void Push(Type type, CloseModel<object[]> ondone = null, params object[] options);

        void Pop();

        void Clear();

        event Action StateChanged;
        public event Action PanalStatusChange;
    }
}
