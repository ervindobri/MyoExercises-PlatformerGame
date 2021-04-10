using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Models
{
    public class Session
    {
        public Session(Subject subject, [NotNull] Dictionary<Exercise, int> exercises)
        {
            Subject = subject;
            Exercises = exercises;
        }

        private Subject Subject { get; set; }
        [NotNull]
        private Dictionary<Exercise, int> Exercises { get; set; }
    }
}