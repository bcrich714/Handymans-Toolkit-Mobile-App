using HandymansToolkit.Services;
using HandymansToolkit.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HandymansToolkit.Services
{
    public static class TaskService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null) return;
            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Task1>();

        }
        public static async Task AddTask(string taskName, string description)
        {
            await Init();
            var task = new Task1
            {
                TaskName = taskName,
                Description = description,
                Status = "Incomplete"
            };

            var id = await db.InsertAsync(task);

        }
        public static async Task RemoveTask(int id)
        {
            await Init();

            await db.DeleteAsync<Task1>(id);

        }
        public static async Task<IEnumerable<Task1>> GetTask()
        {
            await Init();

            var task = await db.Table<Task1>().ToListAsync();
            return task;
        }
    }
}
