using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    public GameObject ValidPlacementDetector;
    private PolygonCollider2D mapPlacementCollider;
    private Vector2 reset;
    void Awake()
    {
        player = new GameData(0, 650, 40);
        shopScript = GameObject.Find("Shop").GetComponent<ShopScript>();
        pauseMenuActive = GameObject.Find("Pause Menu Button").GetComponent<PauseMenuButton>();
        mapPlacementCollider = ValidPlacementDetector.GetComponent<PolygonCollider2D>();
        reset = new Vector2(1000f, 1000f);
    }

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            if (shopScript.dartMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false)
            {
                iconActive = true;
                ResetPositions();
                activeIcon = "DartMonkey";
            }
            else if (shopScript.tackShooterCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false)
            {
                iconActive = true;
                ResetPositions();
                activeIcon = "TackShooter";
            }
            else if (shopScript.iceMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false)
            {
                iconActive = true;
                ResetPositions();
                activeIcon = "IceMonkey";
            }
            else if (shopScript.bombTowerCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false)
            {
                iconActive = true;
                ResetPositions();
                activeIcon = "BombTower";
            }
            else if (shopScript.superMonkeyCollider == Physics2D.OverlapPoint(worldPoint) && pauseMenuActive.turnedOn == false)
            {
                iconActive = true;
                ResetPositions();
                activeIcon = "SuperMonkey";
            }

            Collider2D clickedCollider = Physics2D.OverlapPoint(worldPoint);

            if ((clickedCollider.transform.IsChildOf(ValidPlacementDetector.transform)) && iconActive == true)
            {
                Debug.Log("Clicked valid area");
            }
            else if (clickedCollider != mapPlacementCollider
                && clickedCollider != shopScript.dartMonkeyCollider
                && clickedCollider != shopScript.tackShooterCollider
                && clickedCollider != shopScript.iceMonkeyCollider
                && clickedCollider != shopScript.bombTowerCollider
                && clickedCollider != shopScript.superMonkeyCollider
                && iconActive == true)
            {
                Debug.Log("Invalid placement area");
                iconActive = false;
                ResetPositions();
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

    void ResetPositions()
    {
        DartMonkeySprite.transform.position = reset;
        TackShooterSprite.transform.position = reset;
        IceMonkeySprite.transform.position = reset;
        BombTowerSprite.transform.position = reset;
        SuperMonkeySprite.transform.position = reset;

    }
}
