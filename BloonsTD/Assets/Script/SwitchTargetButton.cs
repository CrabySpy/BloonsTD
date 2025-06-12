using UnityEngine;

public class SwitchTargetingButton : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color hoverColor = new Color(1f, 1f, 1f, 0.9f); // Lighten effect

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

    // ChatGPT used
    private void OnMouseDown()
    {
        Debug.Log("Mouse Down on Switch Button");

        if (UpgradeMenuController.Instance == null)
        {
            Debug.LogWarning("UpgradeMenuController.Instance is null");
            return;
        }

        Tower selectedTower = UpgradeMenuController.Instance.SelectedTower;
        if (selectedTower == null)
        {
            Debug.LogWarning("Selected Tower is null");
            return;
        }

        selectedTower.CycleTargetingMode();
        Debug.Log("Switched Targeting Mode to: " + selectedTower.GetTargetingModeName());
    }

}