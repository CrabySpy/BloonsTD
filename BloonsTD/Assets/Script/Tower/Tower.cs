using UnityEngine;
using UnityEditor;
public abstract class Tower : MonoBehaviour
{
    public TowerInfo towerInfo;
    [SerializeField] private Transform rotationalPoint;


    public void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;
        Handles.DrawWireDisc(transform.position, transform.forward, towerInfo.Range);
        
    }


    public abstract void Attack();
}