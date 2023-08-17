using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Tasks.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand AddTheTask { get; set; }
        public DelegateCommand<object> CheckedTheList { get; set; } 
        public ObservableCollection<string> Tasks { get; set; }

        public MainWindowViewModel()
        {
            AddTheTask = new DelegateCommand(AddingTheTask);
            CheckedTheList = new DelegateCommand<object>(DeleteTheTask);
            Tasks = new ObservableCollection<string>();
        }

        private void DeleteTheTask(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        private void AddingTheTask()
        {
            Tasks.Add(TextBoxText!);
        }

        private string currentDate = DateTime.Now.ToString("MMMM dd', 'dddd");
        public string CurrentDate
        {
            get { return currentDate; }
            set { SetProperty(ref currentDate, value); }
        }

        private string? textBoxText;
        public string? TextBoxText
        {
            get { return textBoxText; }
            set { SetProperty(ref textBoxText, value); }
        }
    }
}
