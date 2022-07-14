using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    // Data Script

    // 몬스터 이름
    [SerializeField]
    private string enemyName;
    
    public string EnemyName { get { return enemyName; } }

    // 몬스터 체력
    [SerializeField]
    private int hp;
    
    public int Hp { get { return hp; } }

    // 몬스터 데미지
    [SerializeField]
    private int damage;
    
    public float Damage { get { return damage; } }

    // 몬스터 이동속도
    [SerializeField]
    private float moveSpeed;
    
    public float MoveSpeed { get { return moveSpeed; } }
}
