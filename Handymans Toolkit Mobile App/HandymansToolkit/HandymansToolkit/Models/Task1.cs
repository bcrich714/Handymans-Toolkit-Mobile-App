using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymansToolkit.Models
{
    //Task Object
    public class Task1
    {
        //Creates a primary key (ID) for the SQLite usage
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
