using KanbaneryStats.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KanbaneryStats.Server.Controllers
{
    [Route("api/[controller]")]
    public class StatsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

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
         
            return taskInfos;
        }
    }
}
