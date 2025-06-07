using UnityEngine;
using TMPro;
using UnityEngine.WSA;
using Unity.Multiplayer.Center.Common;
using System.Linq;

public class ShopScript : MonoBehaviour
{
    public TMP_Text Round;
    public TMP_Text Money;
    public TMP_Text Lives;
    private GameManager gameManager;
    public GameObject InfoBox;
    public GameObject DartMonkeyIcon;
    private BoxCollider2D dartMonkeyCollider;
    public GameObject TackShooterIcon;
    private BoxCollider2D tackShooterCollider;
    public GameObject IceMonkeyIcon;
    private BoxCollider2D iceMonkeyCollider;
    public GameObject BombTowerIcon;
    private BoxCollider2D bombTowerCollider;
    public GameObject SuperMonkeyIcon;
    private BoxCollider2D superMonkeyCollider;

    public GameObject SortButton;
    private BoxCollider2D sortButtonCollider;
    private bool sortButtonActive = false;
    public GameObject SortDropDown;

    public GameObject LeastToMostButton;
    private BoxCollider2D leastToMostButtonCollider;
    private bool leastToMostActive = true;
    public GameObject MostToLeast;

    public GameObject SortCostButton;
    private BoxCollider2D sortCostButtonCollider;
    private bool sortCostActive;
    public GameObject SortCostDisplay;

    public GameObject SortSpeedButton;
    private BoxCollider2D sortSpeedButtonCollider;
    private bool sortSpeedActive;
    public GameObject SortSpeedDisplay;


    public TowerInfo[] TowerInfo;
    private TowerInfo SelectedTower;
    public TMP_Text Tower;
    public TMP_Text Cost;
    public TMP_Text Speed;
    public TMP_Text Desc;

    public SortSearchMethods SortSearchMethods;

    void Start()
    {
        SortSearchMethods = new SortSearchMethods();
        gameManager = FindFirstObjectByType<GameManager>();
        UpdateText();
        dartMonkeyCollider = DartMonkeyIcon.GetComponent<BoxCollider2D>();
        tackShooterCollider = TackShooterIcon.GetComponent<BoxCollider2D>();
        iceMonkeyCollider = IceMonkeyIcon.GetComponent<BoxCollider2D>();
        bombTowerCollider = BombTowerIcon.GetComponent<BoxCollider2D>();
        superMonkeyCollider = SuperMonkeyIcon.GetComponent<BoxCollider2D>();
        sortButtonCollider = SortButton.GetComponent<BoxCollider2D>();
        leastToMostButtonCollider = LeastToMostButton.GetComponent<BoxCollider2D>();
        sortCostButtonCollider = SortCostButton.GetComponent<BoxCollider2D>();
        sortSpeedButtonCollider = SortSpeedButton.GetComponent<BoxCollider2D>();
        SortCostDisplay.SetActive(true);
    }

    void UpdateText()
    {
        Round.text = gameManager.player.Rounds.ToString();
        Money.text = gameManager.player.Money.ToString();
        Lives.text = gameManager.player.Lives.ToString();
    }

    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (dartMonkeyCollider == Physics2D.OverlapPoint(worldPoint))
        {
            InfoBoxMethod(0);
            InfoBox.SetActive(true);
        }
        else if (tackShooterCollider == Physics2D.OverlapPoint(worldPoint))
        {
            InfoBoxMethod(3);
            InfoBox.SetActive(true);
        }
        else if (iceMonkeyCollider == Physics2D.OverlapPoint(worldPoint))
        {
            InfoBoxMethod(4);
            InfoBox.SetActive(true);
        }
        else if (bombTowerCollider == Physics2D.OverlapPoint(worldPoint))
        {
            InfoBoxMethod(1);
            InfoBox.SetActive(true);
        }
        else if (superMonkeyCollider == Physics2D.OverlapPoint(worldPoint))
        {
            InfoBoxMethod(2);
            InfoBox.SetActive(true);
        }
        else
        {
            InfoBox.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (sortButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortButtonActive == false)
            {
                sortButtonActive = true;
                SortDropDown.SetActive(true);
            }
            else if (sortButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortButtonActive == true)
            {
                sortButtonActive = false;
                SortDropDown.SetActive(false);
            }

            if (leastToMostButtonCollider == Physics2D.OverlapPoint(worldPoint) && leastToMostActive == true)
            {
                leastToMostActive = false;
                MostToLeast.SetActive(true);
            }
            else if (leastToMostButtonCollider == Physics2D.OverlapPoint(worldPoint) && leastToMostActive == false)
            {
                leastToMostActive = true;
                MostToLeast.SetActive(false);
            }

            if (sortCostButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortCostActive == false)
            {
                sortCostActive = true;
                sortSpeedActive = false;
                TowerInfo[] sortedTowers = SortSearchMethods.BubbleSort(TowerInfo);
                Debug.Log(string.Join(" -> ", sortedTowers.Select(t => t.Name + "(" + t.Cost + ")")));
                SortCostDisplay.SetActive(true);
                SortSpeedDisplay.SetActive(false);


            }

            if (sortSpeedButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortSpeedActive == false)
            {
                sortCostActive = false;
                sortSpeedActive = true;
                SortCostDisplay.SetActive(false);
                SortSpeedDisplay.SetActive(true);
            }
        }
    }

    void InfoBoxMethod(int index)
    {
        SelectedTower = TowerInfo[index];
        Tower.text = SelectedTower.Name;
        Cost.text = SelectedTower.Cost.ToString();
        Speed.text = SelectedTower.Speed;
        Desc.text = SelectedTower.Description;
    }

   // void UpdatePositions(TowerInfo[] Tower)
   // {
   //     for (int i = 0; i < Tower.Length; i++)
   //     {
   //         if (i = 0)
   //         {
   //             
   //         }
   //     }
   // }
}
