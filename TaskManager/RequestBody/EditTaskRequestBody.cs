using System.Text.Json.Serialization;

namespace TaskManager.RequestBody
{
    public class EditTaskRequestBody
    {
        [JsonPropertyName("taskCode")]
        public string TaskCode { get; set; } = string.Empty;
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("isCompleted")]
        public bool? IsCompleted { get; set; }
    }
}
