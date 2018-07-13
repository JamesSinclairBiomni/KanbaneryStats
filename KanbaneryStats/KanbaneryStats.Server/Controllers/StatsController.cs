using KanbaneryStats.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace KanbaneryStats.Server.Controllers
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private static readonly HttpClient client = new HttpClient();

        private const int doneColumnId = 488704;
        private const int qaColumnId = 488709;
        private const int readyForQaColumnId = 493712;
        private const int actualProjectFieldId = 943;
        private const string baseKanbaneryUrl = "https://biomni.kanbanery.com/api/v1/";

        public StatsController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("[action]")]
        public IEnumerable<TaskInfo> TaskInfo()
        {
            var contentRoot = hostingEnvironment.ContentRootPath;
            var json = System.IO.File.ReadAllText($"{contentRoot}/data/taskinfo.json");
            var taskInfos = JsonConvert.DeserializeObject<List<TaskInfo>>(json);
         
            return taskInfos.OrderBy(t => t.MovedAt);
        }

        [HttpGet("[action]")]
        public DateTime LastUpdated()
        {
            var contentRoot = hostingEnvironment.ContentRootPath;            

            return System.IO.File.GetLastWriteTime($"{contentRoot}/data/taskinfo.json");
        }

        [HttpGet("[action]")]
        public DateTime Refresh(string apiToken)
        {
            var contentRoot = hostingEnvironment.ContentRootPath;

            if (client.DefaultRequestHeaders.Contains("X-KANBANERY-APITOKEN"))
                client.DefaultRequestHeaders.Remove("X-KANBANERY-APITOKEN");
            client.DefaultRequestHeaders.Add("X-KANBANERY-APITOKEN", apiToken);

            var doneKanbaneryTasks = GetKanbaneryTasksForColumn(doneColumnId);
            var qaKanbaneryTasks = GetKanbaneryTasksForColumn(qaColumnId);
            var readyForQaKanbaneryTasks = GetKanbaneryTasksForColumn(readyForQaColumnId);

            var tasks = doneKanbaneryTasks.Where(t => t.EstimateId != null).ToList();
            tasks.AddRange(qaKanbaneryTasks.Where(t => t.EstimateId != null));
            tasks.AddRange(readyForQaKanbaneryTasks.Where(t => t.EstimateId != null));

            var taskInfos = new List<TaskInfo>();
            foreach (var task in tasks) 
            {
                var estimate = GetKanbaneryEstimate(task.EstimateId.Value);
                var actual = GetKanbaneryTaskFields(task.Id).Where(t => t.ProjectFieldId == actualProjectFieldId).ToList();

                if (actual.Any())
                {
                    taskInfos.Add(new TaskInfo { Title = task.Title, Estimate = estimate.Value, Actual = double.Parse(actual.Single().Value), MovedAt = task.MovedAt });
                }
            }

            System.IO.File.WriteAllText($"{contentRoot}/data/taskinfo.json", JsonConvert.SerializeObject(taskInfos));

            return System.IO.File.GetLastWriteTime($"{contentRoot}/data/taskinfo.json");
        }
        
        private List<KanbaneryTask> GetKanbaneryTasksForColumn(int columnId)
        {
            var data = client.GetStringAsync($"{baseKanbaneryUrl}columns/{columnId}/tasks.json").GetAwaiter().GetResult();

            var kanbaneryTasks = JsonConvert.DeserializeObject<List<KanbaneryTask>>(data);

            return kanbaneryTasks;
        }

        private KanbaneryEstimate GetKanbaneryEstimate(int estimateId)
        {
            var data = client.GetStringAsync($"{baseKanbaneryUrl}estimates/{estimateId}.json").GetAwaiter().GetResult();

            var kanbaneryEstimate = JsonConvert.DeserializeObject<KanbaneryEstimate>(data);

            return kanbaneryEstimate;
        }

        private List<KanbaneryTaskField> GetKanbaneryTaskFields(int taskId)
        {
            var data = client.GetStringAsync($"{baseKanbaneryUrl}tasks/{taskId}/addons/task_fields.json").GetAwaiter().GetResult();

            var kanbaneryTaskFields = JsonConvert.DeserializeObject<List<KanbaneryTaskField>>(data);

            return kanbaneryTaskFields;
        }
    }
}
