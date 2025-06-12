using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsButton : MonoBehaviour
{
    public GameObject StatsSceneButton;
    private BoxCollider2D statsButtonCollider;
    void Start()
    {
        statsButtonCollider = StatsSceneButton.GetComponent<BoxCollider2D>();
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (statsButtonCollider == Physics2D.OverlapPoint(worldPoint))
            {
                SceneManager.LoadScene("Stats");
            }
        }
    }
}
