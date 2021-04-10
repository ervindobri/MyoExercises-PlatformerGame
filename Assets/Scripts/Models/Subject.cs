using System;



namespace Models
{
    public enum Sex
    {
        Female, Male, Unspecified 
    }
    public class Subject
    {
        public Subject(string name, int age, Sex sex = Sex.Unspecified )
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

        private String Name { get; set; }
        private int Age { get; set; }
        private Sex Sex { get; set; }
    }
}