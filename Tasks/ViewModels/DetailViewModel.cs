using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Tasks.ViewModels
{
    public class DetailViewModel : BindableBase, IDialogAware
    {
        public string Title => "Details Window";

        public event Action<IDialogResult>? RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
