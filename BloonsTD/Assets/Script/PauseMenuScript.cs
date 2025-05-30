using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public string MainMenu;
    public GameObject BacktoGame;
    private BoxCollider2D backToGameCollider;
    public GameObject BacktoMainMenu;
    private BoxCollider2D backToMainMenuCollider;

    public PauseMenuButton pauseMenuButton;

    void Start()
    {
        backToGameCollider = BacktoGame.GetComponent<BoxCollider2D>();
        backToMainMenuCollider = BacktoMainMenu.GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        //Back to Game Button
        if (Input.GetMouseButtonDown(0))
        {
            //Stack overflow
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
    }
}
