using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public string Gameplay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene(Gameplay);
        }
    }
}
