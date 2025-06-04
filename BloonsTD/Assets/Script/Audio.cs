using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    public static bool MuteMusic = false;
    public static bool MuteSound = false;

    private static Audio instance;

    private AudioSource audioSource;
    private string currentClipName = "";

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;  // loop on start
    }

    public void PlayMusic(string clipName)
    {
        if (clipName == currentClipName)
            return;  // Already playing so do nothing

        AudioClip clip = Resources.Load<AudioClip>(clipName);

        audioSource.clip = clip;
        currentClipName = clipName;
        audioSource.Play();
    }

    void Update()
    {
        audioSource.mute = MuteMusic;

        if (MuteMusic == false && SceneManager.GetActiveScene().name == "Gameplay")
        {
            PlayMusic("GameTheme");
        }
        else
        {
            PlayMusic("MainMenuTheme");
        }
    }
}
