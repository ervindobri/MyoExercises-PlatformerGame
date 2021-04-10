using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class GameplayManager : MonoBehaviour {

    public bool levelUp;
    public static bool doorOpen;

    [Header("GAME UI")]
    public GameObject levelStartUi;
    public GameObject levelCompletedUi;
    public GameObject keyPressedUi;
    public GameObject startButtonUi;

    private UnityEngine.UI.Button startButton;
    private Text currentKey;

    public static bool levelStarted;

    //TODO: make a json with the exercises and parse to game
    //TODO: display exercises and session on UI
    //TODO: start game UI
    //TODO: pause game UI
    private void Awake()
    {
        currentKey = GameObject.FindGameObjectWithTag("CurrentInput").GetComponent<Text>();
        levelStartUi.SetActive(true);
        levelCompletedUi.SetActive(false);
        levelStarted = false;
        startButton = startButtonUi.GetComponent<UnityEngine.UI.Button>();

    }

    void Update ()
    {
        startButton.interactable = InputController.inputLoaded;
        if ( doorOpen && !levelUp) // if score reaches -> load next level
        {
            levelUp = true;
            //SceneManager.LoadScene(loadNextLevel);
        }
	}
    public void LevelCompleted()
    {
        print("Level Completed");
        levelCompletedUi.SetActive(true);
        GameObject.Find("PointsCollected").GetComponent<Text>().text =
            GameObject.Find("PointsText").GetComponent<Text>().text;
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            currentKey.text = e.keyCode.ToString();
        }
    }

    public void StartGame()
    {
        //TODO: start game
        Debug.Log("Game started!");
        levelStartUi.SetActive(false);
        keyPressedUi.SetActive(true);
        levelStarted = true;
        StopWatch.TimerButton();
        BoyController.canMove = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
    }
}
