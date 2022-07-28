using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float MaxHP { get; set; }
    public int Speed { get; set; }
    public int Level { get; set; }


    protected float HP { get; set; }
    
    private void OnEnable()
    {
        Level = 1;

        HP = MaxHP;
    }

    public void Hit(float dmg)
    {
        HP -= dmg;
    }

    public void SetMaxHP(float mHp)
    {
        MaxHP = mHp;
        HP = MaxHP;
    }

    public float GetHP()
    {
        return HP;
    }
}
