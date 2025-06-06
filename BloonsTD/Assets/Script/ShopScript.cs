using UnityEngine;
using TMPro;
using UnityEngine.WSA;
using Unity.Multiplayer.Center.Common;

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

    public TowerInfo[] TowerInfo;
    private TowerInfo SelectedTower;
    public TMP_Text Tower;
    public TMP_Text Cost;
    public TMP_Text Speed;
    public TMP_Text Desc;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        UpdateText();
        dartMonkeyCollider = DartMonkeyIcon.GetComponent<BoxCollider2D>();
        tackShooterCollider = TackShooterIcon.GetComponent<BoxCollider2D>();
        iceMonkeyCollider = IceMonkeyIcon.GetComponent<BoxCollider2D>();
        bombTowerCollider = BombTowerIcon.GetComponent<BoxCollider2D>();
        superMonkeyCollider = SuperMonkeyIcon.GetComponent<BoxCollider2D>();
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
    }

    void InfoBoxMethod(int index)
    {
            SelectedTower = TowerInfo[index];
            Tower.text = SelectedTower.Name;
            Cost.text = SelectedTower.Cost.ToString();
            Speed.text = SelectedTower.Speed;
            Desc.text = SelectedTower.Description;
    }
}
