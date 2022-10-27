using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class Coursefeedback
    {
        public string CourseId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int Rating { get; set; }
        public string? Qualities { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
