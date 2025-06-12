using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonScript : MonoBehaviour
{
    public GameObject ReturnButton;
    private BoxCollider2D returnButtonCollider;
    void Start()
    {
        returnButtonCollider = ReturnButton.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (returnButtonCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
