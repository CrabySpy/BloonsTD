using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButton : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public GameObject pauseMenu;

    public Boolean turnedOn;

    void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Stack overflow
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider == Physics2D.OverlapPoint(worldPoint) && turnedOn == false)
            {
                turnedOn = true;
                pauseMenu.SetActive(true);
            }
            else if (boxCollider == Physics2D.OverlapPoint(worldPoint) && turnedOn == true)
            {
                turnedOn = false;
                pauseMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && turnedOn == false)
        {
            turnedOn = true;
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && turnedOn == true)
        {
            turnedOn = false;
            pauseMenu.SetActive(false);
        }
    }
}