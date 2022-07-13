using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    protected static WaitForSeconds wait = new WaitForSeconds(0.1f);

    public int abilityLV;
    public float damage;
    public float coolTime;
    public string abilityName;
    public string abilityContent;
    public Sprite abilitySprite;

    public Transform player; // 플레이어의 위치
}
