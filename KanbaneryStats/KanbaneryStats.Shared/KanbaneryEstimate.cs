using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryStats.Shared
{
    public class KanbaneryEstimate
    {
        public double Value { get; set; }
        public string Label { get; set; }
        [JsonProperty("project_id")]
        public int ProjectId { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
    }
}
