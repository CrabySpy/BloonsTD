using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenScript : MonoBehaviour
{
    public GameObject LoseScreen;
    public GameObject RestartButton;
    private BoxCollider2D restartButtonCollider;
    public GameObject BackToMainMenuButton2;
    private BoxCollider2D backToMainMenuCollider;
    void Start()
    {
        restartButtonCollider = RestartButton.GetComponent<BoxCollider2D>();
        backToMainMenuCollider = BackToMainMenuButton2.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (restartButtonCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("Gameplay");
            }

            if (backToMainMenuCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }
}
