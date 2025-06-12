using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuController : MonoBehaviour
{
    public static UpgradeMenuController Instance;

    public GameObject upgradeMenu;

    public Tower SelectedTower;

    [Header("UI Texts")]
    public TMP_Text towerNameText;
    public TMP_Text speedText;
    public TMP_Text rangeText;
    public TMP_Text upgrade1NameText;
    public TMP_Text upgrade1CostText;
    public TMP_Text upgrade2NameText;
    public TMP_Text upgrade2CostText;
    public TMP_Text targetingModeText;

    [Header("Upgrade Buttons")]
    public GameObject upgrade1Button;
    public GameObject upgrade2Button;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMenu(Tower tower)
    {
        TowerInfo info = tower.towerInfo;
        SelectedTower = tower;

        towerNameText.text = info.Name;
        speedText.text = info.SpeedString;
        rangeText.text = info.Range.ToString();
        targetingModeText.text = tower.GetTargetingModeName();

        // Upgrade 1
        if (info.Upgrade1Cost > 0)
        {
            upgrade1NameText.text = info.Upgrade1Name;
            upgrade1CostText.text = info.Upgrade1Cost.ToString();
            upgrade1Button.SetActive(true);
        }
        else
        {
            upgrade1NameText.text = "";
            upgrade1CostText.text = "";
            upgrade1Button.SetActive(false);
        }

        // Upgrade 2
        if (info.Upgrade2Cost > 0)
        {
            upgrade2NameText.text = info.Upgrade2Name;
            upgrade2CostText.text = info.Upgrade2Cost.ToString();
            upgrade2Button.SetActive(true);
        }
        else
        {   
            upgrade2NameText.text = "";
            upgrade2CostText.text = "";
            upgrade2Button.SetActive(false);
        }

        upgradeMenu.SetActive(true);
    }

    public void HideMenu()
    {
        upgradeMenu.SetActive(false);
    }

    public void UpdateTargetingModeText(string mode)
    {
        targetingModeText.text = mode;
    }

    public Tower GetSelectedTower()
    {
        return SelectedTower;
    }
}
