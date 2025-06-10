using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData player;
    private ShopScript shopScript;
    private PauseMenuButton pauseMenuActive;
    private bool iconActive;
    private string activeIcon;
    public GameObject DartMonkeySprite;
    public GameObject TackShooterSprite;
    public GameObject IceMonkeySprite;
    public GameObject BombTowerSprite;
    public GameObject SuperMonkeySprite;
    void Awake()
    {
        player = new GameData(0, 650, 40);
        shopScript = GameObject.Find("Shop").GetComponent<ShopScript>();
        pauseMenuActive = GameObject.Find("Pause Menu Button").GetComponent<PauseMenuButton>();
    }

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            if (shopScript.dartMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false && iconActive == false)
            {
                iconActive = true;
                activeIcon = "DartMonkey";

            }
            else if (shopScript.tackShooterCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false && iconActive == false)
            {
                iconActive = true;
                activeIcon = "TackShooter";
            }
            else if (shopScript.iceMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false && iconActive == false)
            {
                iconActive = true;
                activeIcon = "IceMonkey";
            }
            else if (shopScript.bombTowerCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false && iconActive == false)
            {
                iconActive = true;
                activeIcon = "BombTower";
            }
            else if (shopScript.superMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false && iconActive == false)
            {
                iconActive = true;
                activeIcon = "SuperMonkey";
            }
        }

        if (iconActive == true)
        {
            if (activeIcon == "DartMonkey")
            {
                DartMonkeySprite.transform.position = worldPoint;
            }
            else if (activeIcon == "TackShooter")
            {
                TackShooterSprite.transform.position = worldPoint;
            }
            else if (activeIcon == "IceMonkey")
            {
                IceMonkeySprite.transform.position = worldPoint;
            }
            else if (activeIcon == "BombTower")
            {
                BombTowerSprite.transform.position = worldPoint;
            }
            else if (activeIcon == "SuperMonkey")
            {
                SuperMonkeySprite.transform.position = worldPoint;
            }
        }
    }
}
