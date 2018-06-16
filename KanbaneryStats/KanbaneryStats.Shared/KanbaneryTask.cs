using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KanbaneryStats.Shared
{
    public class KanbaneryTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("task_type_id")]
        public int TaskTypeId { get; set; }
        [JsonProperty("column_id")]
        public int ColumnId { get; set; }
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }
        public string Description { get; set; }
        [JsonProperty("estimate_id")]
        public int? EstimateId { get; set; }
        [JsonProperty("owner_id")]
        public int? OwnerId { get; set; }
        public int Position { get; set; }
        public int Priority { get; set; }
        [JsonProperty("ready_to_pull")]
        public bool ReadyToPull { get; set; }
        public bool Blocked { get; set; }
        [JsonProperty("moved_at")]
        public DateTime MovedAt { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public string Type { get; set; }
    }
}
