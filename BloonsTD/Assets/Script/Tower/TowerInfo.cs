using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerInfo", menuName = "Tower Defense/Tower")]
public class TowerInfo : ScriptableObject
{
    [SerializeField] public string Name;
    [SerializeField] public int Cost;
    [SerializeField] private float _speed;
    [SerializeField] public string Speed;
    [SerializeField] private float _range;

    //public List<UpgradeInfo> Upgrades;
    // [SerializeField] private Tower
    [TextArea][SerializeField] public string Description;
    
}
