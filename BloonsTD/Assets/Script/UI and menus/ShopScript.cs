using UnityEngine;
using TMPro;
using UnityEngine.WSA;
using Unity.Multiplayer.Center.Common;
using System.Linq;
using NUnit.Framework.Constraints;

public class ShopScript : MonoBehaviour
{
    public TMP_Text RoundVar;
    public TMP_Text Money;
    public TMP_Text Lives;
    private GameManager gameManager;
    public GameObject InfoBox;
    public GameObject DartMonkeyIcon;
    public BoxCollider2D dartMonkeyCollider;
    public GameObject TackShooterIcon;
    public BoxCollider2D tackShooterCollider;
    public GameObject IceMonkeyIcon;
    public BoxCollider2D iceMonkeyCollider;
    public GameObject BombTowerIcon;
    public BoxCollider2D bombTowerCollider;
    public GameObject SuperMonkeyIcon;
    public BoxCollider2D superMonkeyCollider;

    public GameObject SortButton;
    private BoxCollider2D sortButtonCollider;
    private bool sortButtonActive = false;
    public GameObject SortDropDown;

    public GameObject LeastToMostButton;
    private BoxCollider2D leastToMostButtonCollider;
    public bool leastToMostActive = true;
    public GameObject MostToLeast;

    public GameObject SortCostButton;
    private BoxCollider2D sortCostButtonCollider;
    private bool sortCostActive;
    public GameObject SortCostDisplay;

    public GameObject SortRangeButton;
    private BoxCollider2D sortRangeButtonCollider;
    private bool sortRangeActive;
    public GameObject SortRangeDisplay;


    public TowerInfo[] TowerInfo;
    private TowerInfo SelectedTower;
    public TMP_Text Tower;
    public TMP_Text Cost;
    public TMP_Text Speed;
    public TMP_Text Desc;

    public SortSearchMethods SortSearchMethods;

    private Vector2[] Positions;

    private System.Collections.Generic.Dictionary<TowerInfo, GameObject> towerIconMap;

    private TowerInfo[] currentSortedTowers;
    public WaveManager waveManager;
    public GameObject startRoundButton;
    private BoxCollider2D startRoundButtonCollider;
    public int Rounds = 0;
    public int MoneyVar = 650;
    public int LivesVar = 100;
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
        sortRangeButtonCollider = SortRangeButton.GetComponent<BoxCollider2D>();
        SortCostDisplay.SetActive(true);
        startRoundButtonCollider = startRoundButton.GetComponent<BoxCollider2D>();
        Rounds = 0;
        UpdateRoundText();


        Positions = new Vector2[5]; //storing icon positions into array
        Positions[0] = new Vector2(3.08f, 1.171395f);
        Positions[1] = new Vector2(3.715f, 1.178f);
        Positions[2] = new Vector2(4.4f, 1.19f);
        Positions[3] = new Vector2(5.07f, 1.18f);
        Positions[4] = new Vector2(5.78f, 1.17f);

        //Stack overflow 87-95
        towerIconMap = new System.Collections.Generic.Dictionary<TowerInfo, GameObject>() //maps info from array to icon
        {
            { TowerInfo[0], DartMonkeyIcon },
            { TowerInfo[1], BombTowerIcon },
            { TowerInfo[2], SuperMonkeyIcon },
            { TowerInfo[3], TackShooterIcon },
            { TowerInfo[4], IceMonkeyIcon }
        };

        currentSortedTowers = SortSearchMethods.BubbleSortCost(TowerInfo); //sends in towerinfo to the bubble sort and returns array
        UpdatePositions(currentSortedTowers); //updates positions so icons update to right place

        if (startRoundButton != null)
        {
            startRoundButton.SetActive(true);
        }
    }

    public void UpdateText()
    {
        if (gameManager != null && gameManager.player != null)
        {
            if (RoundVar != null)
                RoundVar.text = gameManager.player.Rounds.ToString();

            if (Money != null)
                Money.text = gameManager.player.Money.ToString();

            if (Lives != null)
                Lives.text = LivesVar.ToString();
        }
        else
        {
            Debug.LogWarning("ShopScript: gameManager or player is null");
        }
    }


    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //Lines 1126-156 chatgpt was used, uses iconmap to change colliders to the right place
        BoxCollider2D hitCollider = Physics2D.OverlapPoint(worldPoint) as BoxCollider2D;

        if (hitCollider != null)
        {
            int index = -1;
            for (int i = 0; i < TowerInfo.Length; i++)
            {
                if (towerIconMap.TryGetValue(TowerInfo[i], out GameObject icon))
                {
                    BoxCollider2D collider = icon.GetComponent<BoxCollider2D>();
                    if (collider == hitCollider)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (index != -1)
            {
                InfoBoxMethod(index);
                InfoBox.SetActive(true);
            }
            else
            {
                InfoBox.SetActive(false);
            }
        }
        else
        {
            InfoBox.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0)) //click condition
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
                ReverseOrder();
                MostToLeast.SetActive(true);
            }
            else if (leastToMostButtonCollider == Physics2D.OverlapPoint(worldPoint) && leastToMostActive == false)
            {
                leastToMostActive = true;
                ReverseOrder();
                MostToLeast.SetActive(false);
            }

            if (sortCostButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortCostActive == false)
            {
                sortCostActive = true;
                sortRangeActive = false;
                currentSortedTowers = SortSearchMethods.BubbleSortCost(TowerInfo);
                if (leastToMostActive == true)
                {
                    UpdatePositions(currentSortedTowers);
                }
                else if (leastToMostActive == false)
                {
                    ReverseOrder();
                }

                SortCostDisplay.SetActive(true);
                SortRangeDisplay.SetActive(false);
            }

            if (startRoundButtonCollider == Physics2D.OverlapPoint(worldPoint))
            {
                TryStartWave();
            }

            if (sortRangeButtonCollider == Physics2D.OverlapPoint(worldPoint) && sortRangeActive == false)
            {
                sortCostActive = false;
                sortRangeActive = true;
                currentSortedTowers = SortSearchMethods.BubbleSortRange(TowerInfo);
                if (leastToMostActive == true)
                {
                    UpdatePositions(currentSortedTowers);
                }
                else if (leastToMostActive == false)
                {
                    ReverseOrder();
                }

                SortCostDisplay.SetActive(false);
                SortRangeDisplay.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryStartWave();
        } 

    }

    void InfoBoxMethod(int index)
    {
        SelectedTower = TowerInfo[index];
        Tower.text = SelectedTower.Name;
        Cost.text = SelectedTower.Cost.ToString();
        Speed.text = SelectedTower.SpeedString;
        Desc.text = SelectedTower.Description;
    }

    void UpdatePositions(TowerInfo[] sortedTowers) //updates icon to right location
    {
        for (int i = 0; i < sortedTowers.Length; i++)
        {
            Vector2 targetPos = Positions[i];
            //Stack overflow line 247
            if (towerIconMap.TryGetValue(sortedTowers[i], out GameObject icon))
            {
                icon.transform.localPosition = targetPos;
            }
        }
    }

    void ReverseOrder() // reverses order of sorted array
    {
        TowerInfo[] reverse = new TowerInfo[currentSortedTowers.Length];
        for (int i = 0; i < currentSortedTowers.Length; i++)
        {
            reverse[i] = currentSortedTowers[currentSortedTowers.Length - 1 - i];
        }
        UpdatePositions(reverse);

        currentSortedTowers = reverse;
    }
    
    public void IncrementRound() {
        Rounds++;
        if (Rounds != 1)
        {
            MoneyVar = MoneyVar + 101;
        }
        UpdateRoundText();
    }

    public void UpdateRoundText()
    {
        if (RoundVar != null)
        {
            RoundVar.text = Rounds.ToString();
        }

        if (Money != null)
        {
            Money.text = MoneyVar.ToString();
        }
    }
    
    public void TryStartWave()
    {
        if (waveManager != null)
        {
            waveManager.StartNextWave(this);
            if (startRoundButton != null)
            {
                startRoundButton.SetActive(false); // hide button while wave runs
            }
        }
    }

    public void OnWaveComplete() {
        if (startRoundButton != null)
        {
            startRoundButton.SetActive(true); // re-enable when wave finishes
        }
    }

}


