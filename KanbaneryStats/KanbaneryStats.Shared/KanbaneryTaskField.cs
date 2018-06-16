using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KanbaneryStats.Shared
{
    public class KanbaneryTaskField
    {
        public double Value { get; set; }
        [JsonProperty("project_field_id")]
        public int ProjectFieldId { get; set; }
        [JsonProperty("task_id")]
        public int TaskId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("value_field_id")]
        public int ValueFieldId { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
    }
}
