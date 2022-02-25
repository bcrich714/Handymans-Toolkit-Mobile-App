using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HandymansToolkit.Models;
using HandymansToolkit.Services;

namespace HandymansToolkit.ViewModels
{
    class TaskPlannerViewModel : BaseViewModel
    {
        //Task Storage (stores the tasks into an observable list)
        public ObservableRangeCollection<Task1> Task1 { get; set; }
        public ObservableRangeCollection<Grouping<string, Task1>> TaskGroups { get; set; }

        //Commands
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddTaskCommand { get; }

        public AsyncCommand<Task1> RemoveTaskCommand { get; }

        //Constructor/Initializer
        public TaskPlannerViewModel()
        {
            //Initialising the list and title
            Task1 = new ObservableRangeCollection<Task1>();
            TaskGroups = new ObservableRangeCollection<Grouping<string, Task1>>();
            Title = "Task Planner";

            //Initialise the groupings of the list (FROM PREVIOUS ITERATIONS)
            TaskGroups.Add(new Grouping<string, Task1>("Incomplete", Task1.Where(t => t.Status == "Incomplete")));
            TaskGroups.Add(new Grouping<string, Task1>("Complete", Task1.Where(t => t.Status == "Complete")));

            //Command Initializers
            RefreshCommand = new AsyncCommand(Refresh);
            AddTaskCommand = new AsyncCommand(AddTask);
            RemoveTaskCommand = new AsyncCommand<Task1>(RemoveTask);
        }

        //Refreshes the page and displays the list of tasks
        //NOTE: there is a 2 second delay to better show that something is happening
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Task1.Clear();
            var tasks = await TaskService.GetTask();
            Task1.AddRange(tasks);
            IsBusy = false;
        }

        //Removes a task from the list, refreshes the feed
        async Task RemoveTask(Task1 task)
        {
            await TaskService.RemoveTask(task.Id);
            await Refresh();
        }

        //Adds a task to the list, refreshes feed
        //NOTE: task name and task description cannot be null values
        async Task AddTask()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Task Name", "Name of Task");
            var description = await App.Current.MainPage.DisplayPromptAsync("Task Description", "Description of task");
            if (name.Equals("") || description.Equals(""))
            {
                await App.Current.MainPage.DisplayAlert("Invalid Entry", "Task Name and/or Description cannot be blank", "OK");
                return;
            }
            await TaskService.AddTask(name, description);
            await Refresh();
        }
    }
}
