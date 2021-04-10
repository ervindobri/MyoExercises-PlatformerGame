using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoyCollect : MonoBehaviour {

    //Changing player sprites color
    [Header("Sprite color")]
    public SpriteRenderer playerSpr;
    public Color collideColor, collideColor2;
    private Animator anim;

    //Points:
    [Header("Points")]
    public int points = 0;
    private bool justCollected;

    public Text pointsText;
    public GameObject[] chests;
    List<Animator> chestAnimators;

    //Sounds:
    [Header("Sounds")]
    public AudioClip coinCollect;
    public AudioClip chestOpened;
    public AudioClip levelClip;
    private int score;

    //GameManager
    public GameplayManager gameplayManager;
    public StopWatch stopWatch;
    private void Awake()
    {
        anim = GameObject.Find("Boy").GetComponent<Animator>();
        chestAnimators = new List<Animator>();
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i] = GameObject.Find("Chest-"+i);
        }
        
        if ( chests.Length > 0)
        {
            for (int i = 0; i < chests.Length; i++)
            {
                chestAnimators.Add(chests[i].GetComponent<Animator>());
                chestAnimators[i].enabled = false;
            }
        }
        else
        {
            return;
        }
    }
    //determine which chest gonna open
    public void FindChestAndOpenIt(string chestName)
    {
        if ( chests.Length > 0)
        {
            for (int i = 0; i < chests.Length; i++)
            {
                if ( chests[i].name == chestName)
                {
                    chestAnimators[i].enabled = true;
                    chestAnimators[i].SetTrigger("Open");
                    if (!chestAnimators[i].GetBool("isOpen"))
                    {
                        points += 100;
                        print("Chest opened!");
                        chestAnimators[i].SetBool("isOpen", true);
                        SoundManager.instance.PlaySoundFx(chestOpened, .3f);
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    // Update is called once per frame
    void Update () {
        pointsText.text = points.ToString();
        if ( points > 0 && justCollected)
        {
            StartCoroutine(PlayerCollect());
            justCollected = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.transform.tag == "Coin")
        {
            points += 20;
            Destroy(target.gameObject);
            justCollected = true;
        }
        if (target.transform.tag == "Chest")
        {
            FindChestAndOpenIt(target.gameObject.name);
            
        }
        if (target.transform.tag == "Door")
        {
            if ( CheckScore())
            {
                GameplayManager.doorOpen = true;
                // opens door.
                //level up -> loading next scene
                //display level completed UI-image
                //freeze character movement.
                anim.SetFloat("Speed", 0);
                gameplayManager.LevelCompleted();
                StopWatch.TimerButton();
                SoundManager.instance.PlaySoundFx(levelClip, 0.3f);

                Invoke("disableSoundFxManagers", 1);
            }
        }
    }
    void disableSoundFxManagers( )
    {
        SoundManager.instance.soundFxManager.enabled = false;
        SoundManager.instance.soundFxManager2.enabled = false;

    }
    bool CheckScore()
    {
        if ( int.TryParse(pointsText.text,out score) && score >= 200  )
        {
            return true;
        }
        return false;
    }

    IEnumerator PlayerCollect()
    {
        SoundManager.instance.PlaySoundFx(coinCollect, .2f); // play sound effect
        for (int i = 0; i < 2; i++)
        {
            playerSpr.color = collideColor;
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i < 2; i++)
        {
            playerSpr.color = collideColor2;
            yield return new WaitForSeconds(.1f);
            playerSpr.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }
}
