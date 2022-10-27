using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Professorfeedbacks = new HashSet<Professorfeedback>();
        }

        public string ProfessorName { get; set; } = null!;
        public int? Rating { get; set; }

        public virtual ICollection<Professorfeedback> Professorfeedbacks { get; set; }
    }
}
