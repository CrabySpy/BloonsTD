using NUnit.Framework;
using UnityEngine;

public enum TowerSpeed
{
    Slow,
    Medium,
    Fast,
    Hyperonic
};

[CreateAssetMenu(fileName = "TowerInfo", menuName = "Tower Defense/Tower")]
public class TowerInfo : ScriptableObject
{
    public string Name;
    public int Cost;
    public TowerSpeed Speed;
    public float Range;

    public string Upgrade1Name;
    public int Upgrade1Cost;

    public string Upgrade2Name;
    public int Upgrade2Cost;

    [TextArea]public string Description;


    public string SpeedString
    {
        get
        {
            switch (Speed)
            {
                case TowerSpeed.Slow: return "Slow";
                case TowerSpeed.Medium: return "Medium";
                case TowerSpeed.Fast: return "Fast";
                case TowerSpeed.Hyperonic: return "Hyperonic";
                default: return "Medium";
            }
        }
    }
}