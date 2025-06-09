using UnityEngine;
using UnityEditor;

public abstract class Tower : MonoBehaviour
{
    public TowerInfo towerInfo;
    [SerializeField] private Transform rotationalPoint;
    [SerializeField] private LayerMask bloonsMask;
    [SerializeField] protected Transform firePoint;

    private bool isTargetInRange = false;
    private float attackCooldown;
    private float attackInterval;

    private bool showRange = false;

    protected Animator animator;
    private bool isAttacking = false;
    protected string ATTACK_STRING = "Attack";

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
    }

    private void Update()
    {
        CheckMouseTarget();
        ToggleRange();

        if (isTargetInRange)
        {
            // Debug.Log($"{gameObject.name}: Mouse in range, rotating and attacking.");

            RotateTowardsMouse();

            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                isAttacking = true;
                animator?.SetBool(ATTACK_STRING, isAttacking);
                // Debug.Log($"{gameObject.name}: Attacking!");
                Attack();
                attackCooldown = attackInterval;
            }
        }
        else
        {
            if (isAttacking)
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

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    // Toggle showing the range circle
                    showRange = !showRange;
                    Debug.Log($"showing {gameObject.name} range");
                }
                else
                {
                    // Optional: Hide range if clicked elsewhere
                    showRange = false;
                }
            }
        }
    }
    

    private void ToggleRange()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            showRange = !showRange;
            Debug.Log($"Toggle Range: {showRange}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (towerInfo != null)
        {
            if (showRange)
            {
                Handles.color = Color.white;
                Handles.DrawWireDisc(transform.position, Vector3.forward, towerInfo.Range);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (towerInfo != null)
        {
            if (showRange)
            {
                Handles.color = Color.white;
                Handles.DrawWireDisc(transform.position, Vector3.forward, towerInfo.Range);
            }
        }
    }

    public abstract void Attack();
}
