using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCursor : Ability
{
    private SpriteRenderer spriteRenderer;
 
    private Vector2 posFix;
    public Transform target;

    public float yPosition;
    // 반지름.
    public float radius = 1.0f;
    // 회전 속도.
    public float angularVelocity = 480.0f;
    // 위치.
    public float angle = 0.0f;


    private void Awake()
    {
        transform.localScale = new Vector3(30, 30, 0);
        damage = 5.0f;
        posFix = new Vector2(3f, 0.6f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Logic()
    {
        Transform playerLogic = player.transform.GetChild(0);

        StartCoroutine(Round());
    }

    private IEnumerator Round()
    {
        while (true)
        {
            transform.position = player.position + Vector3.left * posFix.x + Vector3.down * posFix.y;

            // 회전 각도.
            angle += angularVelocity * Time.deltaTime;
            // 오프셋 위치.
            Vector3 offset = Quaternion.Euler(0.0f, angle, 0.0f) * new Vector3(0.0f, 0.0f, radius);
            // 이펙트 위치.
            transform.position = new Vector3(target.transform.position.x, yPosition, target.transform.position.z) + offset;

            yield return wait;
            ObjectPool.Instance.ReturnObject(gameObject);
        }
    }
}
