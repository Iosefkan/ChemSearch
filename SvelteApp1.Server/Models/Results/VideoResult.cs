using SvelteApp1.Server.Data;

namespace SvelteApp1.Server.Models.Results
{
    public class VideoResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Topic { get; set; }
        public uint Grade { get; set; }
    }
}
