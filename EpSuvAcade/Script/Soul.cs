using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public float soulAmount;
    public float getRange;

    private GameObject player;

    private void Awake()
    {
        // 아이템을 획득 할 플레이어
        player = GameObject.FindGameObjectWithTag("character");
    }

    private void LateUpdate()
    {
        if ((player.transform.position - transform.position).magnitude >= 80)  // 플레이어와 너무 멀어지면 오브젝트 풀로 돌아감
        {
            ObjectPool.Instance.ReturnObject(gameObject);
        }
        else
            return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "character")     // 플레이어가 근처에 있을때
        {
            Vector3 dir = (collision.transform.transform.position - gameObject.transform.position).normalized;

            if ((gameObject.transform.position - collision.transform.transform.position).magnitude > getRange)  // 아이템이 캐릭터와 거리가 있을때
            {
                gameObject.transform.Translate(dir * Time.deltaTime * 30.0f);  // 플레이어한테 이동, 플레이어 속도보다는 빨라야 함
            }
            else
            {
                Player player = collision.GetComponentInParent<Player>();   // Player 객체의 Player.cs를 가져옴
                player.LevelUp(soulAmount);

                // 오브젝트 풀로 돌아감
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }
}
