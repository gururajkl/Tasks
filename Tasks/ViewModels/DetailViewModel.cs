using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Tasks.Models;

namespace Tasks.ViewModels
{
    public class DetailViewModel : BindableBase, IDialogAware
    {
        public string Title => "Details Window";

        private string? dateTime;
        public string? DateTime
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }
        }

        private string? taskName;
        public string? TaskName
        {
            get { return taskName; }
            set { SetProperty(ref taskName, value); }
        }

        public event Action<IDialogResult>? RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            // Nothing need to be done.
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var p = parameters.GetValue<ModelTask>("model");
            DateTime = $"Task Added At {p.AddedAt:g}";
            TaskName = p.TaskName;
        }
    }
}
