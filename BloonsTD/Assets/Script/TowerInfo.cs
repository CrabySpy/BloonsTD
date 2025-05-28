using UnityEngine;

[CreateAssetMenu(fileName = "TowerInfo", menuName = "Tower Infomation", order = 0)]
public class TowerInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    // [SerializeField] private Tower
    [TextArea][SerializeField] private string _description;
    
}
