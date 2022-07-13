using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : Ability
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    
    private bool isFilp;

    private void Awake()
    {
        transform.localScale = new Vector3(3, 3, 0);
        damage = 0.4f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private Vector2 FireVec()
    {
        // 플레이어 주변 가장 가까운 적으로의 방향 반환 

        RaycastHit2D[] rayHit = Physics2D.CircleCastAll(player.position, 20f, Vector2.zero, 0f, LayerMask.GetMask("Monster"));
        if (rayHit != null)
        {
            Vector3 curVec = player.position;
            float distance = Mathf.Infinity;
            Vector2 vec = Vector2.zero;
            foreach (var obj in rayHit)
            {
                if (distance > (obj.collider.transform.position - curVec).magnitude)
                {
                    distance = (obj.collider.transform.position - curVec).magnitude;
                    vec = obj.collider.transform.position - player.position;
                }
            }
            return vec.normalized;
        }
        return Vector2.zero;
    }

    public void Logic()
    {
        Vector2 vec = FireVec();
        
        if (vec == Vector2.zero) {
            ObjectPool.Instance.ReturnObject(gameObject);
            return;
        }  // 주위의 적이 없으면 그냥 없어짐

        transform.position = player.position;
        
        if (vec.x < 0) {
            isFilp = true;
            spriteRenderer.flipX = false;
        }

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(vec * 15, ForceMode2D.Impulse);

        StartCoroutine(BulletDone());
        
    }

    private IEnumerator BulletDone()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(0.6f);
        rigid.velocity = Vector2.zero;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(3.0f);
        anim.SetBool("isAttack", false);

        if (isFilp) {
            isFilp = false;
            spriteRenderer.flipX = true;
        }

        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
