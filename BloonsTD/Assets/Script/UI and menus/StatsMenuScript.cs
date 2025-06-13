using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SavableStats // class for the savable users stats
{
    public string Name;
    public int Value;

    public SavableStats(string name, int value)
    {
        this.Name = name;
        this.Value = value;
    }
}
public class StatsMenuScript : MonoBehaviour
{
    public TMP_Text VariableDisplay;
    private SavableStats[] stats;
    private int index;
    public GameObject HighestWave;
    private BoxCollider2D highestWaveCollider;
    public GameObject TotalWavesPlayed;
    private BoxCollider2D totalWavesPlayedCollider;
    public GameObject TotalTowersPlaced;
    private BoxCollider2D totalTowersPlacedCollider;
    public GameObject TotalBalloonsPopped;
    private BoxCollider2D totalBalloonsPoppedCollider;
    void Start()
    {
        highestWaveCollider = HighestWave.GetComponent<BoxCollider2D>();
        totalWavesPlayedCollider = TotalWavesPlayed.GetComponent<BoxCollider2D>();
        totalTowersPlacedCollider = TotalTowersPlaced.GetComponent<BoxCollider2D>();
        totalBalloonsPoppedCollider = TotalBalloonsPopped.GetComponent<BoxCollider2D>();
        stats = new SavableStats[4];
        //Placeholder stats, actual stats will be entered from gamemanager variables
        stats[0] = new SavableStats("HighestWave", 27);
        stats[1] = new SavableStats("TotalWavesPlayed", 56);
        stats[2] = new SavableStats("totalTowersPlaced", 180);
        stats[3] = new SavableStats("TotalBalloonsPlaced", 783);
    }

    void Update() // logic for buttons, if clicked with left click
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (highestWaveCollider == Physics2D.OverlapPoint(worldPoint))
            {
                index = BinarySearch(stats, "HighestWave"); //send array and target
                VariableDisplay.text = stats[index].Value.ToString(); // return value to the text
            }
            else if (totalWavesPlayedCollider == Physics2D.OverlapPoint(worldPoint))
            {
                index = BinarySearch(stats, "TotalWavesPlayed");
                VariableDisplay.text = stats[index].Value.ToString();
            }
            else if (totalTowersPlacedCollider == Physics2D.OverlapPoint(worldPoint))
            {
                index = BinarySearch(stats, "TotalTowersPlaced");
                VariableDisplay.text = stats[index].Value.ToString();
            }
            else if (totalBalloonsPoppedCollider == Physics2D.OverlapPoint(worldPoint))
            {
                index = BinarySearch(stats, "TotalBalloonsPlaced");
                VariableDisplay.text = stats[index].Value.ToString();
            }
        }
    }

    public int BinarySearch(SavableStats[] stats, string targetName) //standard binary search, finds location of index
    {
        int targetLength = targetName.Length;
        int left = 0;
        int right = stats.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (stats[mid].Name.Length == targetLength) //using the length of the name variable to determine markers
            {
                return mid;
            }
            else if (targetLength < stats[mid].Name.Length)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }
        return -1; // not found case
    }
}
