using UnityEngine;

public class PowerUpCrate : MonoBehaviour
{
    public int moneyReward = 100;
    public float lifetime = 0.25f;

    public void InitializeLifetime()
    {
        Destroy(gameObject, lifetime);
    }

    public void ActivatePowerUp()
    {
        Debug.Log("power up collected");

        GameManager gm = FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            gm.player.Money += moneyReward;
        }

        ShopScript shop = FindFirstObjectByType<ShopScript>();
        if (shop != null)
        {
            shop.UpdateText();
        }

        Destroy(gameObject);
    }
}
