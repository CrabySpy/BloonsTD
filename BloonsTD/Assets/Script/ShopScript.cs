using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public TMP_Text Round;
    public TMP_Text Money;
    public TMP_Text Lives;
    private GameManager gameManager;
    public GameObject InfoBox;
    public GameObject DartMonkeyIcon;
    private BoxCollider2D dartMonkeyCollider;

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
            InfoBox.SetActive(true);
        }
        else
        {
            InfoBox.SetActive(false);
        }
    }
}
