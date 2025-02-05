using System.Text.Json.Serialization;

namespace TaskManager.RequestBody
{
    public class DeleteTaskRequestBody
    {
        [JsonPropertyName("taskCode")]
        public string TaskCode { get; set; } = string.Empty;
    }
}
