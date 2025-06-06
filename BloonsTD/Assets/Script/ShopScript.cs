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

    public TowerInfo[] TowerInfo;
    public TowerInfo SelectedTower;
    public TMP_Text Tower;
    public TMP_Text Cost;
    public TMP_Text Speed;
    public TMP_Text Desc;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        UpdateText();
        dartMonkeyCollider = DartMonkeyIcon.GetComponent<BoxCollider2D>();
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
            InfoBoxMethod("Dart Monkey");
            InfoBox.SetActive(true);
        }
        else
        {
            InfoBox.SetActive(false);
        }
    }

    void InfoBoxMethod(string tower)
    {
        if (tower == "Dart Monkey")
        {
            SelectedTower = TowerInfo[0];

            Tower.text = SelectedTower.Name;
            Cost.text = SelectedTower.Cost.ToString();
            Speed.text = SelectedTower.Speed;
            Desc.text = SelectedTower.Description;
        }
    }
}
