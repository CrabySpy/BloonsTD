using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData player;
    void Awake()
    {
        player = new GameData(0, 0, 40);
    }

    void Update()
    {
        
    }
}
