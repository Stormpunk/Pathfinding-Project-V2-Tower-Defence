using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    private BaseTower thisTower;
    #region Attack Delay Times
    private float attackDelay, damageInterval;
    #endregion
    public float TowerRange { get; private set; }
    private CircleCollider2D circleCollider; // Reference to the CircleCollider2D component
    // Start is called before the first frame update
    void Start()
    {
        attackDelay = thisTower.attackTime;
        damageInterval = thisTower.damageInterval;
        TowerRange = thisTower.towerRange;

        // Get the CircleCollider2D component attached to the tower
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = TowerRange; // Set the radius of the circle collider to the tower's range
    }

    // Update is called once per frame
    void Update()
    {
        // Use Physics2D.OverlapCircleAll method if needed
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Mon") && attackDelay == damageInterval)
            {
                GameObject enemy = col.gameObject;
                enemy.GetComponent<Mons>().DamageMe(1);
                attackDelay = 0;
            }
        }
        #region Attack Timer Delay
        attackDelay += Time.deltaTime;
        if (attackDelay >= damageInterval)
        {
            attackDelay = damageInterval;
        }
        #endregion
    }
}
