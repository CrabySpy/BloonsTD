using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public float lifetime = 3f;
    public int moneyReward = 100;

    private float timer;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void ActivatePowerUp()
    {
        Debug.Log("power up collected");
        FindObjectOfType<GameManager>().player.Money += moneyReward;
        Destroy(gameObject);
    }
}
