using System;
using UnityEngine;

namespace Models
{
    public class Exercise
    {
        public Exercise(string instructions, string code, string name, KeyCode key)
        {
            Instructions = instructions;
            Code = code;
            Name = name;
            Key = key;
        }

        private String Name { get; set; }
        private String Code { get; set; }
        private String Instructions { get; set; }
        private KeyCode Key { get; set; }
    }
}

