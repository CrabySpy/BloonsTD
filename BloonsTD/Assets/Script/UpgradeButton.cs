using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color hoverColor = new Color(1f, 1f, 1f, 0.9f);

    private ShopScript shopScript;

    public int upgradeIndex; // 0 = Faster Shooting, 1 = Longer Range

    void Awake()
    {
        shopScript = GameObject.Find("Shop").GetComponent<ShopScript>();
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalColor = sr.color;
        }
    }

    private void OnMouseEnter()
    {
        if (sr != null)
            sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (sr != null)
            sr.color = originalColor;
    }

    private void OnMouseDown()
    {
        Tower selectedTower = UpgradeMenuController.Instance.GetSelectedTower();
        TowerInfo info = selectedTower.towerInfo;

        // ChatGPT used
        if (selectedTower != null)
        {
            if (upgradeIndex == 0 && shopScript.MoneyVar >= info.Upgrade1Cost) 
            {
                selectedTower.ApplyUpgrade(upgradeIndex);
                shopScript.MoneyVar = shopScript.MoneyVar - info.Upgrade1Cost;
                shopScript.UpdateRoundText();
            }
            else if (upgradeIndex == 1 && shopScript.MoneyVar >= info.Upgrade2Cost)
            {
                selectedTower.ApplyUpgrade(upgradeIndex);
                shopScript.MoneyVar = shopScript.MoneyVar - info.Upgrade2Cost;
                shopScript.UpdateRoundText();
            }
            else
            {
                Debug.Log("Not enough money for upgrade!");
                return;
            }
            
        }

        Debug.Log($"{gameObject.name} clicked! Applying upgrade index {upgradeIndex}");
    }
}
