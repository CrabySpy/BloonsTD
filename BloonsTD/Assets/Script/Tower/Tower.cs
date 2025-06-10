using UnityEngine;
using UnityEditor;

public abstract class Tower : MonoBehaviour
{
    public TowerInfo towerInfo;
    [SerializeField] private Transform rotationalPoint;
    [SerializeField] private LayerMask bloonsMask;
    [SerializeField] protected Transform firePoint;

    [SerializeField] private GameObject rangeCircle;
    private static Tower currentlySelectedTower = null;


    private bool isTargetInRange = false;
    private float attackCooldown;
    private float attackInterval;

    // private bool showRange = false;

    protected Animator animator;
    private bool isAttacking = false;
    protected string ATTACK_STRING = "Attack";

    //private TargetingMode targetingMode = TargetingMode.First;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        if (towerInfo.SpeedString == "Slow")
        {
            attackInterval = 1f;
        }
        else if (towerInfo.SpeedString == "Medium")
        {
            attackInterval = 0.7f;
        }
        else if (towerInfo.SpeedString == "Fast")
        {
            attackInterval = 0.4f;
        }
        else if (towerInfo.SpeedString == "Hyperonic")
        {
            attackInterval = 0.1f;
        }
        
        if (rangeCircle != null && towerInfo != null)
        {
        // Because default sprite size = 1 unit radius, so scale to match towerInfo.Range:
        float diameter = towerInfo.Range * 2f;
        rangeCircle.transform.localScale = new Vector3(diameter, diameter, 1f);

        // Make sure it starts hidden
        rangeCircle.SetActive(false);
        }
    }

    protected void Update()
    {
        CheckMouseTarget();

        if (isTargetInRange)
        {
            // Debug.Log($"{gameObject.name}: Mouse in range, rotating and attacking.");

            RotateTowardsMouse();

            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                if (animator != null)
                {
                    isAttacking = true;
                    animator.SetBool(ATTACK_STRING, isAttacking);
                }
                
                // Debug.Log($"{gameObject.name}: Attacking!");
                Attack();
                attackCooldown = attackInterval;
            }
        }
        else
        {
            if (isAttacking && animator != null)
            {
                isAttacking = false;
                animator?.SetBool(ATTACK_STRING, isAttacking); // STOP animating
            }
            // Debug.Log($"{gameObject.name}: Mouse out of range.");
        }
    }

    private void CheckMouseTarget()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        float distance = Vector2.Distance(transform.position, mouseWorldPos);
        isTargetInRange = distance <= towerInfo.Range;

        // Debug.Log($"{gameObject.name}: Mouse distance = {distance:F2}, InRange = {isTargetInRange}");
    }

    protected virtual void RotateTowardsMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 direction = mouseWorldPos - rotationalPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Debug.Log($"{gameObject.name}: Rotating toward angle {angle:F1}°");
    }

    // private Bloon SelectTarget() 
    // {
    //     // List<Bloon> bloons = GetBloonsInRange();
    //     // return TargetingSystem.GetTarget(bloons, transform, targetingMode);
    // }
    
    // Returns a list of bloons within the tower's range (serach algrithm)
    // private List<Bloon> GetBloonsInRange()
    // {
    //     Collider[] hits = Physics.OverlapSphere(transform.position, towerInfo.Range, bloonsMask);
    //     List<Bloon> bloons = new List<Bloon>();

    //     foreach (var hit in hits)
    //     {
    //         Bloon bloon = hit.GetComponent<Bloon>();
    //         if (bloon != null && !bloon.IsPopped)
    //         {
    //             bloons.Add(bloon);
    //         }
    //     }

    //     return bloons;
    // }
    private void OnMouseDown()
    {
        // If clicking on this tower but it's not currently selected:
        if (currentlySelectedTower != this)
        {
            // Hide the previous selection's range
            if (currentlySelectedTower != null)
                currentlySelectedTower.HideRange();

            // Set this as the new selection
            currentlySelectedTower = this;
            ShowRange();
        }
        else
        {
            // Clicking the same tower again toggles it off
            HideRange();
            currentlySelectedTower = null;
        }
    }

    private void ShowRange()
    {
        if (rangeCircle != null)
            rangeCircle.SetActive(true);
    }

    private void HideRange()
    {
        if (rangeCircle != null)
            rangeCircle.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (towerInfo != null)
        {
                Handles.color = Color.white;
                Handles.DrawWireDisc(transform.position, Vector3.forward, towerInfo.Range);
        }
    }

    public abstract void Attack();

    // public abstract void AnimationAttack();
}