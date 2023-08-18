using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Media;
using System.Windows;

namespace Tasks.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand AddTheTask { get; set; }
        public DelegateCommand TextboxFocused { get; set; }
        public DelegateCommand<object> CheckedTheList { get; set; }
        public DelegateCommand ClearCompletedTask { get; set; }
        public ObservableCollection<string> Tasks { get; set; }
        public ObservableCollection<string> CompletedTasks { get; set; }

        public MainWindowViewModel()
        {
            AddTheTask = new DelegateCommand(AddingTheTask, canExecute).ObservesProperty(() => TextBoxText);
            CheckedTheList = new DelegateCommand<object>(DeleteTheTask);
            TextboxFocused = new DelegateCommand(ClearTheTB);
            ClearCompletedTask = new DelegateCommand(Clear);
            Tasks = new ObservableCollection<string>();
            CompletedTasks = new ObservableCollection<string>();
        }

        private bool canExecute()
        {
            if (string.IsNullOrEmpty(TextBoxText) || TextBoxText == "＋ Add Task") return false;
            return true;
        }

        private void Clear()
        {
            CompletedTasks.Clear();
            UpdateUI();
        }

        private void ClearTheTB()
        {
            TextBoxText = string.Empty;
        }

        private void DeleteTheTask(object obj)
        {
            string? value = obj as string;
            Tasks.Remove(value!);
            CompletedTasks.Add(value!);
            UpdateUI();
            PlayBellSound();
        }

        private void AddingTheTask()
        {
            Tasks.Add(TextBoxText!);
            TextBoxText = string.Empty;
            UpdateUI();
        }

        private string currentDate = DateTime.Now.ToString("MMMM dd', 'dddd");
        public string CurrentDate
        {
            get { return currentDate; }
            set { SetProperty(ref currentDate, value); }
        }

        private string? textBoxText = "＋ Add Task";
        public string? TextBoxText
        {
            get { return textBoxText; }
            set { SetProperty(ref textBoxText, value); }
        }

        private string? headingText = "Tasks";
        public string? HeadingText
        {
            get { return headingText; }
            set { SetProperty(ref headingText, value); }
        }

        private Visibility completedTaskVisibility = Visibility.Collapsed;
        public Visibility CompletedTaskVisibility
        {
            get { return completedTaskVisibility; }
            set { SetProperty(ref completedTaskVisibility, value); }
        }

        public void UpdateUI()
        {
            if (Tasks.Count > 0)
                HeadingText = $"{Tasks.Count} Tasks Yet to complete";
            else HeadingText = "Tasks";

            if (CompletedTasks.Count > 0)
                CompletedTaskVisibility = Visibility.Visible;
            else CompletedTaskVisibility = Visibility.Collapsed;
        }

        public static void PlayBellSound()
        {
            string soundFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bell.wav");
            SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
            soundPlayer.Play();
        }

        private void ShowNotification(string title, string message)
        {
        }
    }
}
