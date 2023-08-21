using System;

namespace Tasks.Models
{
    public class ModelTask
    {
        public int Id { get; set; }
        public string? TaskName { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
