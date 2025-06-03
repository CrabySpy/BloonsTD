using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerInfo", menuName = "Tower Defense/Tower")]
public class TowerInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    //public List<UpgradeInfo> Upgrades;
    // [SerializeField] private Tower
    [TextArea][SerializeField] private string _description;
    
}
