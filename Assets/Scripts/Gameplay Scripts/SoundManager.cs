using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioSource soundFxManager, soundFxManager2;
    public AudioSource themeSongManager;
    public AudioClip[] themeSongs;

    private void Awake()
    {
        MakeSingleton();
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
        if (!themeSongManager.isPlaying)
        {
            themeSongManager.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            themeSongManager.volume = 0.1f;
            themeSongManager.Play();
        }
    }
    public void PlaySoundFx(AudioClip audioClip, float volume)
    {
        if (!soundFxManager.isPlaying)
        {
            soundFxManager.clip = audioClip;
            soundFxManager.volume = volume;
            soundFxManager.Play();
        }
        else
        {
            soundFxManager2.clip = audioClip;
            soundFxManager2.volume = volume;
            soundFxManager2.Play();
        }
    }
    public void PlayRandomSoundFx(AudioClip[] audioClips)
    {
        if (!soundFxManager.isPlaying)
        {
            soundFxManager.clip = audioClips[Random.Range(0, audioClips.Length)];
            soundFxManager.volume = Random.Range(.5f, .8f);
            soundFxManager.Play();
        }
        else
        {
            soundFxManager2.clip = audioClips[Random.Range(0, audioClips.Length)];
            soundFxManager.volume = Random.Range(.5f, .8f);
            soundFxManager2.Play();
        }
    }
}
