using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour {
    public float timeStart;
    public Text timeText;

    static bool timerActive = false;

    private void Awake()
    {
        timerActive = false;
    }

    void Start () {
        timeText.text = timeStart.ToString("F2");
    }
	
	// Update is called once per frame
	void Update () {
        if (timerActive)
        {
            timeStart += Time.deltaTime;
            timeText.text = timeStart.ToString("F2");
        }
    }
    public static void TimerButton()
    {
        timerActive = !timerActive;
    }
}
