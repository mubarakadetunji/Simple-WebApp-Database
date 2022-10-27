using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class Course
    {
        public Course()
        {
            Coursefeedbacks = new HashSet<Coursefeedback>();
        }

        public string CourseId { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Prerequisites { get; set; }
        public string? Antirequisites { get; set; }
        public int Units { get; set; }
        public string? Notes { get; set; }
        public int? Rating { get; set; }

        public virtual ICollection<Coursefeedback> Coursefeedbacks { get; set; }
    }
}
