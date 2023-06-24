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
    public static float TowerRange { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = thisTower.attackTime;
        damageInterval = thisTower.damageInterval;
        TowerRange = thisTower.towerRange;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, thisTower.towerRange);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Mon") && attackDelay == damageInterval)
            {
                GameObject enemy = col.gameObject;
                enemy.GetComponent<Mons>().DamageMe(1);
                attackDelay = 0;
            }
            else
            {
                Debug.Log("Can't attack yet");
            }
        }
        #region Attack Timer Delay
        attackDelay += Time.deltaTime;
        if (attackDelay  >= damageInterval)
        {
            attackDelay = damageInterval;
        }
        #endregion
    }
}
