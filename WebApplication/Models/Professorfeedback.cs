using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class Professorfeedback
    {
        public string ProfessorName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int Rating { get; set; }
        public string? Qualities { get; set; }
        public bool? Approved { get; set; }

        public virtual Professor ProfessorNameNavigation { get; set; } = null!;
        public virtual User UsernameNavigation { get; set; } = null!;
    }
}
