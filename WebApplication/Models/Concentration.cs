using System;
using System.Collections.Generic;

namespace Scheduler.Models
{
    public partial class Concentration
    {
        public string ConcentrationName { get; set; } = null!;
        public string Requirements { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
