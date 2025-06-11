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

    public GameObject pretabDart;
    public GameObject pretabTack;
    public GameObject pretabIce;
    public GameObject pretabBomb;
    public GameObject pretabSuper;
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
                if (activeIcon == "DartMonkey" && shopScript.MoneyVar >= 250)
                {
                    GameObject newPretab = Instantiate(pretabDart);

                    newPretab.transform.position = worldPoint;

                    newPretab.name = "DartMonkeyPretab";

                    iconActive = false;
                    shopScript.MoneyVar = shopScript.MoneyVar - 250;
                    shopScript.UpdateRoundText();
                    ResetPositions();
                }
                else if (activeIcon == "TackShooter" && shopScript.MoneyVar >= 400)
                {
                    GameObject newPretab = Instantiate(pretabTack);

                    newPretab.transform.position = worldPoint;

                    newPretab.name = "DartMonkeyPretab";

                    iconActive = false;
                    shopScript.MoneyVar = shopScript.MoneyVar - 400;
                    shopScript.UpdateRoundText();
                    ResetPositions();
                }
                else if (activeIcon == "IceMonkey" && shopScript.MoneyVar >= 850)
                {
                    GameObject newPretab = Instantiate(pretabIce);

                    newPretab.transform.position = worldPoint;

                    newPretab.name = "DartMonkeyPretab";

                    iconActive = false;
                    shopScript.MoneyVar = shopScript.MoneyVar - 850;
                    shopScript.UpdateRoundText();
                    ResetPositions();
                }
                else if (activeIcon == "SuperMonkey" && shopScript.MoneyVar >= 4000)
                {
                    GameObject newPretab = Instantiate(pretabSuper);

                    newPretab.transform.position = worldPoint;

                    newPretab.name = "DartMonkeyPretab";

                    iconActive = false;
                    shopScript.MoneyVar = shopScript.MoneyVar - 4000;
                    shopScript.UpdateRoundText();
                    ResetPositions();
                }
                else if (activeIcon == "BombTower" && shopScript.MoneyVar >= 900)
                {
                    GameObject newPretab = Instantiate(pretabBomb);

                    newPretab.transform.position = worldPoint;

                    newPretab.name = "DartMonkeyPretab";

                    iconActive = false;
                    shopScript.MoneyVar = shopScript.MoneyVar - 900;
                    shopScript.UpdateRoundText();
                    ResetPositions();
                }
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
