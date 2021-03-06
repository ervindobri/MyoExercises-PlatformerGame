using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Models;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class InputController : MonoBehaviour
{
    [Header("Texts and button")]
    public Text nameText;
    public Text age;
    public Text status;
    public Text exercises;
    public GameObject start;
    private Button startButton;

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
    // public List<JsonExercise> exercises;
    public static bool inputLoaded = false;
    public GameObject exercisePrefab;
    public GameObject parent;
    private Vector3 pos;

    
    public Patient player;
    public static Patient Player;
    public Session session;
    
    private void Awake()
    {
        // pos = GameObject.Find("Labels").GetComponent<Transform>().localPosition;
        // Debug.Log(pos);
        nameText.text = "Ervin";
        age.text = "5";
        startButton = start.GetComponent<Button>();
    }

    public void LoadPlayerData()
    {
        try
        {
            var resultAge = int.Parse(
                Regex.Match(age.text, @"\d+").Value
            );
            Debug.Log("Name:" + nameText.text + ", Age:" + resultAge);
            Player = FirestoreLoader.Patients
                .Where(x => x.Name.Contains(nameText.text) && x.Age.Equals(resultAge))
                .ToList().FirstOrDefault();
            session = FirestoreLoader.Session;
            if (Player != null)
            {
                status.text = "status - success";
                exercises.text = $"exercises - {Player.Exercises.Count}";
                player = Player;
                inputLoaded = true;
                startButton.interactable = true;
            }
        }
        catch (Exception e)
        {
            status.text = $"status - {e.Message}";
            startButton.interactable = (false);
        }
        
    }
    // public void LoadMappedKeys()
    // {
    //     //TODO: load json and assign keys according
    //     var jsonFile = Resources.Load(nameInput.text) as TextAsset;
    //     Assert.IsNotNull(jsonFile);
    //     Debug.Log(jsonFile.text);
    //
    //     SubjectKeys keysJson = JsonUtility.FromJson<SubjectKeys>(jsonFile.text);
    //     
    //     Debug.Log(keysJson.name);
    //     exercises = keysJson.exercises.ToList();
    //
    //
    //     var gos = GameObject.FindGameObjectsWithTag("ExerciseInfo");
    //     foreach (var go in gos)
    //     {
    //         DestroyImmediate(go);
    //     }
    //     for (int i = 0; i <= exercises.Count-1; i++)
    //     {
    //         // Instantiate at position (0, 0, 0) and zero rotation.
    //         GameObject go = Instantiate(exercisePrefab, Vector3.zero, Quaternion.identity);
    //         go.transform.parent = parent.transform;
    //         go.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    //         go.transform.localPosition = new Vector3(pos.x + (i+1)*125, pos.y, 0);
    //         var texts = go.GetComponentsInChildren<Text>();
    //         texts[0].text = exercises[i].name;
    //         texts[1].text = exercises[i].assigned_key;
    //     }
    //     inputLoaded = true;
    // }
}
