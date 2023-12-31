﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Forms;
using Tasks.Models;

namespace Tasks.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand AddTheTask { get; set; }
        public DelegateCommand TextboxFocused { get; set; }
        public DelegateCommand<object> CheckedTheList { get; set; }
        public DelegateCommand ClearCompletedTask { get; set; }
        public DelegateCommand<object> ClickedOnListItem { get; set; }
        public ObservableCollection<ModelTask> Tasks { get; set; }
        public ObservableCollection<ModelTask> CompletedTasks { get; set; }

        public static int incrementTheCount = 1;
        private readonly IDialogService dialogService;
        public static string? HeadingTextForNotifyIcon = "";
        private NotifyIcon? systemTrayIcon;

        public MainWindowViewModel(IDialogService? dialogService)
        {
            AddTheTask = new DelegateCommand(AddingTheTask, canExecute).ObservesProperty(() => TextBoxText);
            CheckedTheList = new DelegateCommand<object>(DeleteTheTask);
            TextboxFocused = new DelegateCommand(ClearTheTB);
            ClearCompletedTask = new DelegateCommand(Clear);
            ClickedOnListItem = new DelegateCommand<object>(DetailViewOfTask);
            Tasks = new ObservableCollection<ModelTask>();
            CompletedTasks = new ObservableCollection<ModelTask>();

            this.dialogService = dialogService!;
            CreateTaskTrayIcon();
        }

        private void DetailViewOfTask(object SelectedTask)
        {
            DialogParameters p = new DialogParameters();
            p.Add("model", SelectedTask as ModelTask);

            dialogService.ShowDialog("Detail", p, r =>
            {
                // Feature yet to add.
            });
        }

        private bool canExecute()
        {
            if (string.IsNullOrEmpty(TextBoxText)) return false;
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
            if (obj != null)
            {
                ModelTask? selectedList = obj as ModelTask;
                Tasks.Remove(selectedList!);
                CompletedTasks.Add(selectedList!);
                UpdateUI();
                PlayBellSound();
                systemTrayIcon!.ShowBalloonTip(2000, "Task Completed", $"You Just Completed {selectedList!.TaskName}", ToolTipIcon.Info);
            }
        }

        private void AddingTheTask()
        {
            ModelTask newTask = new ModelTask() { TaskName = TextBoxText, Id = incrementTheCount++ };
            Tasks.Add(newTask);
            TextBoxText = string.Empty;
            UpdateUI();
            systemTrayIcon!.ShowBalloonTip(2000, "Task Added", $"You added {newTask.TaskName} to your task list", ToolTipIcon.Info);
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

        public void CreateTaskTrayIcon()
        {
            systemTrayIcon = new NotifyIcon();
            systemTrayIcon.Icon = new System.Drawing.Icon("checklist_106575.ico");
            systemTrayIcon.Text = "Tasks";
            systemTrayIcon.Click += SystemTrayIconClick;
            systemTrayIcon.Visible = true;
        }

        private void SystemTrayIconClick(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Maximized;
            System.Windows.Application.Current.MainWindow.Activate();
        }
    }
}
