using Microsoft.EntityFrameworkCore;
using System.IO;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoDatabase : DbContext
    {
        public string DbPath { get; }

        public ToDoDatabase(DbContextOptions<ToDoDatabase> options) : base (options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=ToDo.db");

        public DbSet<ToDoItem> ToDos { get; set; }

    }

    
}
