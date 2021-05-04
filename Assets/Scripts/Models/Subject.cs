using System;
using System.Collections;
using System.Collections.Generic;


namespace Models
{
    public class Patient
    {
        public static Patient FromSnapshot(Dictionary<string, object> snapshot)
        {
            Dictionary<string, object> exercises = new Dictionary<string, object>();
            foreach (object item in (IEnumerable) snapshot["exercises"])
            {
                var x = item is KeyValuePair<string, object> ? (KeyValuePair<string, object>) item : default;
                exercises.Add(x.Key, x.Value);
            }
            return new Patient()
            {
                Name = snapshot["name"].ToString(),
                Age = int.Parse(snapshot["age"].ToString()),
                Exercises = exercises 
            };
        }

        public Patient(){}
        public Patient(string name, int age, Dictionary<string, object> exercises)
        {
            Name = name;
            Age = age;
            Exercises = exercises;
        }

        public String Name { get; set; }
        public int Age { get; set; }

        public Dictionary<string, object> Exercises { get; set; }
    }
}