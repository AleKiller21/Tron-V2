﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ValidationReport
    {
        public ValidationStatus Status { get; set; }

        public Player CrashTarget { get; set; }

        public ValidationReport(ValidationStatus status, Player crashTarget)
        {
            Status = status;
            CrashTarget = crashTarget;
        }
    }
}
