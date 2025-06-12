using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IceMonkey : Tower
{
    [SerializeField] private float slowAmount = 0.5f; // 50% speed
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private int damage = 1;

    public override void Attack()
    {
        List<Bloon> targets = Physics2D.OverlapCircleAll(transform.position, towerInfo.Range, bloonsMask)
            .Select(hit => hit.GetComponent<Bloon>())
            .Where(b => b != null && b.isClone)
            .ToList();

        foreach (Bloon bloon in targets)
        {
            bloon.TakeDamage(damage);
            StartCoroutine(ApplySlow(bloon));
        }
    }

    protected override void RotateTowardsTarget(Bloon target)
    {
        // Ice Monkey doesn't rotate, but override required
    }

    private IEnumerator ApplySlow(Bloon bloon)
    {
        float originalSpeed = bloon.speed;
        bloon.speed *= slowAmount;

        yield return new WaitForSeconds(slowDuration);

        if (bloon != null)
        {
            bloon.speed = originalSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, towerInfo.Range);
    }
}
