using System.Collections.Generic;

namespace Models
{
    [System.Serializable]
    public class JsonExercise
    {
        public string name ;
        public string code ;
        public string instruction ;
        public string assigned_key;
    }

    [System.Serializable]
    public class SubjectKeys
    {
        public string name ;
        public List<JsonExercise> exercises ;
    }
}