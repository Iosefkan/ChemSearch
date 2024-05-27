namespace SvelteApp1.Server.Models.Requests
{
    public class FilterRequest
    {
        public int[]? Topicid { get; set; }
        public int[]? GradeId { get; set; }
        public string? Name { get; set; }
    }
}
