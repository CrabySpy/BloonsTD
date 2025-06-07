using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string MainMenu;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            //Stack overflow
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider == Physics2D.OverlapPoint(worldPoint))
        {
                SceneManager.LoadScene(MainMenu);
            }
        }
    }
}
