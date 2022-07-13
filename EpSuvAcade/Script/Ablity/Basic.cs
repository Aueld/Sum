using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Ability
{
    private SpriteRenderer spriteRenderer;
 
    private Vector2 posFix;
    private bool isFilp;

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
        
        if (playerLogic.rotation.y == 0)
            isFilp = true;
        else
            isFilp = false;

        StartCoroutine(BasicAttack());
    }

    private IEnumerator BasicAttack()
    {
        if (isFilp)
        {
            transform.position = player.position + Vector3.right * posFix.x + Vector3.down * posFix.y;
            spriteRenderer.flipY = true;

            yield return wait;

            transform.position = player.position + Vector3.left * posFix.x + Vector3.down * posFix.y;
            spriteRenderer.flipY = false;
        }
        else
        {
            transform.position = player.position + Vector3.left * posFix.x + Vector3.down * posFix.y;
            spriteRenderer.flipY = false;

            yield return wait;
            
            transform.position = player.position + Vector3.right * posFix.x + Vector3.down * posFix.y;
            spriteRenderer.flipY = true;
        }

        yield return wait;
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
