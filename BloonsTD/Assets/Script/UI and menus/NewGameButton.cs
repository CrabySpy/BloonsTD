using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    private BoxCollider2D boxColliderMM;

    void Start(){
        boxColliderMM = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            //Stack overflow
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (boxColliderMM == Physics2D.OverlapPoint(worldPoint))
        {
                SceneManager.LoadScene("Gameplay");
            }
        }
    }
}
