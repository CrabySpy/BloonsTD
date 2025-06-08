using UnityEngine;
using UnityEditor;

public abstract class Tower : MonoBehaviour
{
    public TowerInfo towerInfo;
    [SerializeField] private Transform rotationalPoint;
    [SerializeField] private LayerMask bloonsMask;
    [SerializeField] protected Transform firePoint;


    private Transform target;

    private void Update()
    {
        // if (target == null)
        // {
        //     FindTarget();
        // }
        // else
        // {
        //     if (Vector2.Distance(transform.position, target.position) > towerInfo.Range)
        //     {
        //         target = null; // Lose target if out of range
        //         return;
        //     }

            // RotateTowardsTarget();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // private void FindTarget()
    // {
    //     Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, towerInfo.Range, bloonsMask);

    //     foreach (Collider2D hit in hits)
    //     {
    //         target = hit.transform;
    //         break; // Just pick the first one
    //     }
    // }

    // public void RotateTowardsTarget()
    // {
    //     if (target == null) return;

    //     Vector2 direction = target.position - rotationalPoint.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

    //     rotationalPoint.rotation = Quaternion.Slerp(rotationalPoint.rotation, targetRotation, 3 * Time.deltaTime);
    // }

    private void OnDrawGizmosSelected()
    {
        if (towerInfo != null)
        {
            Handles.color = Color.white;
            Handles.DrawWireDisc(transform.position, Vector3.forward, towerInfo.Range);
        }
    }

    public virtual void Attack() 
    {
        // Implement attack logic in derived classes
        Debug.Log("Attack method called in " + gameObject.name);
    }
}
