using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float MaxHP { get; set; }
    public int Speed { get; set; }
    
    protected float HP { get; set; }

    private void OnEnable()
    {
        HP = MaxHP;
    }

    public void Hit(float dmg)
    {
        HP -= dmg;
    }

    public float GetHP()
    {
        return HP;
    }
}
