using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Scriptables", menuName = "Towers")]
public class BaseTower : ScriptableObject
{
    public float towerRange;
    #region Damage Time and Amount
    [SerializeField]
    private int damage;
    public float attackTime, damageInterval;
    //the timer between attacks and the number that the timer must reach before it can attack again
    #endregion
    [SerializeField]
    private Mons[] engagedMons;

    //Get the engagedMons Array
    public Mons[] EngagedMons => engagedMons;
}
