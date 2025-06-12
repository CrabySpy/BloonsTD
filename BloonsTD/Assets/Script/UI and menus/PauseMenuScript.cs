using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject BacktoGame;
    private BoxCollider2D backToGameCollider;
    public GameObject BacktoMainMenu;
    private BoxCollider2D backToMainMenuCollider;

    public GameObject ResetGame;
    private BoxCollider2D resetGameCollider;

    public GameObject MuteMusic;
    private BoxCollider2D muteMusicCollider;
    public GameObject MuteMusicHider;

    public GameObject MuteSound;
    private BoxCollider2D muteSoundCollider;
    public GameObject MuteSoundHider;

    public PauseMenuButton pauseMenuButton;

    void Start()
    {
        backToGameCollider = BacktoGame.GetComponent<BoxCollider2D>();
        backToMainMenuCollider = BacktoMainMenu.GetComponent<BoxCollider2D>();
        muteMusicCollider = MuteMusic.GetComponent<BoxCollider2D>();
        muteSoundCollider = MuteSound.GetComponent<BoxCollider2D>();
        resetGameCollider = ResetGame.GetComponent<BoxCollider2D>();
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

        //Back to Game Button
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (backToGameCollider == Physics2D.OverlapPoint(worldPoint))
            {
                pauseMenuButton.turnedOn = false;
                gameObject.SetActive(false);
            }
        }

        //Back to Main Menu Button
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (backToMainMenuCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        //Reset game button
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (resetGameCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("Gameplay");
            }
        }

        //Mute Music Button
        if (Input.GetMouseButtonDown(0))
        {
            //Stack overflow
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

        //Mute Sound Button
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
