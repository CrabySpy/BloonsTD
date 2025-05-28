using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string MainMenu;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene(MainMenu);
        }
    }
}

