using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class User
    {
        public User()
        {
            Coursefeedbacks = new HashSet<Coursefeedback>();
            Professorfeedbacks = new HashSet<Professorfeedback>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string SchoolEmail { get; set; } = null!;
        public string? CoursesTaken { get; set; }
        public string? Concentrations { get; set; }
        public int? Funds { get; set; }
        public bool? Registered { get; set; }
        public bool? isAdmin { get; set; }

        public virtual ICollection<Coursefeedback> Coursefeedbacks { get; set; }
        public virtual ICollection<Professorfeedback> Professorfeedbacks { get; set; }
    }
}
