using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{
    public TowerInfo towerInfo;
    [SerializeField] private Transform rotationalPoint;
    [SerializeField] private LayerMask bloonsMask;
    [SerializeField] protected Transform firePoint;

    private GameObject rangeCircle;
    private static Tower currentlySelectedTower = null;

    private float attackCooldown;
    private float attackInterval;

    protected Animator animator;
    private bool isAttacking = false;
    protected string ATTACK_STRING = "Attack";

    [SerializeField] private TargetingMode targetingMode = TargetingMode.First;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        SetAttackInterval();
        CreateRangeCircle();
    }

    private void SetAttackInterval()
    {
        if (towerInfo.SpeedString == "Slow") attackInterval = 1.5f;
        else if (towerInfo.SpeedString == "Medium") attackInterval = 1f;
        else if (towerInfo.SpeedString == "Fast") attackInterval = 0.7f;
        else if (towerInfo.SpeedString == "Hyperonic") attackInterval = 0.2f;
    }

    private void CreateRangeCircle()
    {
        rangeCircle = new GameObject("RangeCircle");
        rangeCircle.transform.position = transform.position;
        rangeCircle.transform.rotation = Quaternion.identity;

        var sr = rangeCircle.AddComponent<SpriteRenderer>();
        sr.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        sr.color = new Color(225f / 255f, 225f / 255f, 225f / 255f, 0.2f);
        sr.sortingLayerName = "Towers";
        sr.sortingOrder = 1;

        float spriteDiameterUnits = sr.sprite.bounds.size.x;
        float desiredDiameter = towerInfo.Range * 2f;
        float scaleFactor = desiredDiameter / spriteDiameterUnits;

        rangeCircle.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        rangeCircle.SetActive(false);
    }

    protected void Update()
    {
        List<Bloon> bloonsInRange = GetBloonsInRange();
        Bloon target = TargetingSystem.GetTarget(bloonsInRange, transform, targetingMode);

        if (target != null)
        {
            RotateTowardsTarget(target);

            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                if (animator != null)
                {
                    isAttacking = true;
                    animator.SetBool(ATTACK_STRING, isAttacking);
                }

                Attack();
                attackCooldown = attackInterval;
            }
        }
        else
        {
            if (isAttacking && animator != null)
            {
                isAttacking = false;
                animator.SetBool(ATTACK_STRING, isAttacking);
            }
        }

        if (rangeCircle != null)
        {
            rangeCircle.transform.position = transform.position;
        }
    }

    private List<Bloon> GetBloonsInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, towerInfo.Range, bloonsMask);
        List<Bloon> bloons = new List<Bloon>();

        foreach (var hit in hits)
        {
            Bloon bloon = hit.GetComponent<Bloon>();
            if (bloon != null)
            {
                bloons.Add(bloon);
            }
        }

        return bloons;
    }

    
    protected virtual void RotateTowardsTarget(Bloon target)
    {
        if (rotationalPoint == null || target == null) return;

        Vector3 dir = target.transform.position - rotationalPoint.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnMouseDown()
    {
        if (currentlySelectedTower != this)
        {
            if (currentlySelectedTower != null)
                currentlySelectedTower.HideRange();

            currentlySelectedTower = this;
            ShowRange();
        }
        else
        {
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
}
