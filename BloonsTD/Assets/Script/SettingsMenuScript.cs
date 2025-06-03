using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuButtons : MonoBehaviour
{
    public GameObject Return;
    private BoxCollider2D returnCollider;

    public GameObject MuteMusicButton;
    private BoxCollider2D muteMusicCollider;
    public GameObject MuteMusicHider;

    public GameObject MuteSoundButton;
    private BoxCollider2D muteSoundCollider;
    public GameObject MuteSoundHider;
    void Start()
    {
        returnCollider = Return.GetComponent<BoxCollider2D>();
        muteMusicCollider = MuteMusicButton.GetComponent<BoxCollider2D>();
        muteSoundCollider = MuteSoundButton.GetComponent<BoxCollider2D>();

        if (Audio.MuteMusic == true)
        {
            MuteMusicHider.SetActive(false);
        }

        if (Audio.MuteSound == true)
        {
            MuteSoundHider.SetActive(false);
        }
    }

    void Update()
    {
        //Return button
        if (Input.GetMouseButtonDown(0))
        {
            //Stack overflow
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (returnCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        //Save button

        //Hotkeys button

        //Mute music button
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (muteMusicCollider == Physics2D.OverlapPoint(worldPoint) && Audio.MuteMusic == false)
            {
                Audio.MuteMusic = true;
                MuteMusicHider.SetActive(false);
            }

            else if (muteMusicCollider == Physics2D.OverlapPoint(worldPoint))
            {
                Audio.MuteMusic = false;
                MuteMusicHider.SetActive(true);
            }
        }

        //Mute sounds button
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (muteSoundCollider == Physics2D.OverlapPoint(worldPoint) && Audio.MuteSound == false)
            {
                Audio.MuteSound = true;
                MuteSoundHider.SetActive(false);
            }

            else if (muteSoundCollider == Physics2D.OverlapPoint(worldPoint))
            {
                Audio.MuteSound = false;
                MuteSoundHider.SetActive(true);
            }
        }
    }
}
