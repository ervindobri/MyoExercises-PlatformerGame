using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    public Dictionary<string, KeyCode> supportedKeys = new Dictionary<string, KeyCode>(){
        {"UP", KeyCode.UpArrow },
        {"DOWN", KeyCode.DownArrow },
        {"LEFT", KeyCode.LeftArrow },
        {"RIGHT", KeyCode.RightArrow },
        {"SPACE", KeyCode.Space },
        {"W", KeyCode.W },
        {"A", KeyCode.A },
        {"S", KeyCode.S },
        {"D", KeyCode.D },
        {"NONE", KeyCode.None },
    };

    public InputField nameInput;
    public List<JsonExercise> exercises;
    public static bool inputLoaded = false;
    public GameObject exercisePrefab;
    public GameObject parent;
    private Vector3 pos;

    private void Awake()
    {
        pos = GameObject.Find("Labels").GetComponent<Transform>().localPosition;
        Debug.Log(pos);
        nameInput.text = "ERvin";
    }

    public void LoadMappedKeys()
    {
        //TODO: load json and assign keys according
        var jsonFile = Resources.Load(nameInput.text) as TextAsset;
        Assert.IsNotNull(jsonFile);
        Debug.Log(jsonFile.text);

        SubjectKeys keysJson = JsonUtility.FromJson<SubjectKeys>(jsonFile.text);
        
        Debug.Log(keysJson.name);
        exercises = keysJson.exercises.ToList();


        var gos = GameObject.FindGameObjectsWithTag("ExerciseInfo");
        foreach (var go in gos)
        {
            DestroyImmediate(go);
        }
        for (int i = 0; i <= exercises.Count-1; i++)
        {
            // Instantiate at position (0, 0, 0) and zero rotation.
            GameObject go = Instantiate(exercisePrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = parent.transform;
            go.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            go.transform.localPosition = new Vector3(pos.x + (i+1)*125, pos.y, 0);
            var texts = go.GetComponentsInChildren<Text>();
            texts[0].text = exercises[i].name;
            texts[1].text = exercises[i].assigned_key;
        }
        inputLoaded = true;
    }
}
