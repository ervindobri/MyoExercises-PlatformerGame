using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Models
{
    public class Session
    {
        public Session(){}
        public Session(int reps, int pause)
        {
            Reps = reps;
            Pause = pause;
        }

        public int Reps { get; set; }
        public int Pause { get; set; }

        public static Session FromSnapshot(Dictionary<string, object> sessionSnapshot)
        {
            return new Session()
            {
                Reps = int.Parse(sessionSnapshot["reps"].ToString()),
                Pause = int.Parse(sessionSnapshot["pause"].ToString()),
            };
        }
    }
}