using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryStats.Shared
{
    public class TaskInfo
    {
        public string Title { get; set; }
        public double Estimate { get; set; }
        public double Actual { get; set; }
        public double Factor => Actual/Estimate;
    }
}
