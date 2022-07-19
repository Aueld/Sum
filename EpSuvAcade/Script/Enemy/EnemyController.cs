using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private readonly static WaitForSeconds wait = new WaitForSeconds(0.1f);

    [SerializeField] private EnemyData enemyData;

    private Player playerLogic;         // 플레이어 스크립트
    private Rigidbody2D rb;             // 물리값
    private SpriteRenderer spriteRenderer; // 스프라이트 이미지

    private float playerHitTime = 0.2f; // 적 한명에게 맞고 그 다음 맞을 때 시간
    private float curHp;                // 변경된 체력
    private float BigBulletHitTime;     // 장판 피격 딜레이

    public GameManager gm;              // 게임매니저
    public Text DestroyEnemyCount;      // 처치된 몬스터
    public GameObject player;           // 플레이어
    public GameObject canvas;           // 체력바가 있는 캔버스
    public Image hpBar;                 // 체력바

    private bool isHit;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // 오브젝트 풀에 의해 다시 활성화되면 초기화
    private void OnEnable()   
    {
        curHp = enemyData.Hp;
        hpBar.fillAmount = (float)curHp / enemyData.Hp;
        canvas.SetActive(false);
    }
    
    private void Update()
    {
        if (!isHit) // 피격시 잠시 정지, 멀리 날아가는것 방지
            rb.velocity = Vector2.zero;

        Move();
    }

    private void Move()
    {
        // 플레이어에게로 이동
        Vector3 movement = (player.transform.position - transform.position).normalized;
        transform.Translate(movement * enemyData.MoveSpeed * Time.deltaTime);

        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        if((player.transform.position - transform.position).magnitude >= 80)  // 적과 너무 멀어지면 다시 플레이어 주변으로 순간이동
        {
            transform.position =  gm.RandomPosition();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ability Multi Hit")   // 멀티 다단 히트
        {
            try
            {
                playerLogic = player.GetComponent<Player>();
                float abilityDamage = collision.GetComponent<Ability>().damage + playerLogic.playerDamage;

                if (gameObject.activeSelf)
                {
                    StopCoroutine(HideHp());
                    StartCoroutine(HideHp());
                }
                
                canvas.SetActive(true);

                Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
                transform.position.y - collision.transform.position.y).normalized;

                BigBulletHitTime += Time.deltaTime;                 // 다단히트 딜레이
                if (BigBulletHitTime > 0.5f)
                {
                    try
                    {
                        if (gameObject.activeSelf)
                        {
                            StartCoroutine(Hit(reactVec, abilityDamage));
                        }
                        BigBulletHitTime = 0f;
                    }
                    catch
                    {
                        ObjectPool.Instance.ReturnObject(gameObject);
                    }
                }
            }
            catch
            {
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)     // 적 히트
    {
        if (collision.tag == "ability") // 능력에 피격
        {
            try
            {
                playerLogic = player.GetComponent<Player>();
                float abilityDamage = collision.GetComponent<Ability>().damage + playerLogic.playerDamage;

                if (gameObject.activeSelf)
                {
                    StopCoroutine(HideHp());
                    StartCoroutine(HideHp());
                }
                canvas.SetActive(true);

                Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
                transform.position.y - collision.transform.position.y).normalized;

                try
                {
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(Hit(reactVec, abilityDamage));
                    }
                }
                catch
                {
                    ObjectPool.Instance.ReturnObject(gameObject);
                }
            }
            catch
            {
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)   // 플레이어 피격
    {
        if(collision.gameObject.tag == "character")
        {
            try
            {
                playerHitTime += Time.deltaTime;
                if (playerHitTime >= 0.2f)      // 피격 딜레이
                {
                    playerHitTime = 0.0f;
                    playerLogic = player.GetComponent<Player>();
                    playerLogic.PrintPlayerHp(enemyData.Damage);
                }
            }
            catch
            {

            }
        }
    }

    private IEnumerator HideHp()    // 10초 동안 피격 당하지 않을시 체력바 숨김
    {
        yield return new WaitForSeconds(10.0f);

        canvas.SetActive(false);
    }

    private IEnumerator Hit(Vector2 reactVec, float damage) // 피격
    {
        curHp -= damage;


        GameObject dmgTextObj = ObjectPool.Instance.GetObject("DmgText");       // 데미지 텍스트
        dmgTextObj.transform.position = transform.position + Vector3.up * 3f;
        dmgTextObj.SetActive(true);

        TextMeshPro dmgTextObjText = dmgTextObj.GetComponent<TextMeshPro>();
        dmgTextObjText.text = ((int)damage).ToString();


        hpBar.fillAmount = (float)curHp / enemyData.Hp;

        if (curHp > 0)  // 체력 0 초과일때
        {
            isHit = true;
            rb.AddForce(reactVec * 5f, ForceMode2D.Impulse);
            spriteRenderer.color = Color.red;
            yield return wait;
            spriteRenderer.color = Color.white;
            isHit = false;

        }
        else            // 체력 0 이하일때
        {
            int ran = Random.Range(0, 10);

            var obj = ObjectPool.Instance.GetObject("RedSoul");

            if (ran > 1)    // 80% 확률로 드랍
            {
                obj.transform.position = gameObject.transform.position;
                obj.SetActive(true);
            }

            if (transform.gameObject.name == "Monster_Boss(Clone)")   // 보스일때 처치시 상위 아이템 드랍
            {
                for (int i = 0; i < 20; i++)
                {
                    obj = ObjectPool.Instance.GetObject("BlueSoul");
                    obj.transform.position = gameObject.transform.position
                         + (Vector3) (Random.insideUnitCircle + new Vector2(Random.Range(0.02f, 0.05f), Random.Range(0.02f, 0.05f)));
                    obj.SetActive(true);
                }
            }

            gm.DestroyEnemyCount();

            ObjectPool.Instance.ReturnObject(gameObject);
            spriteRenderer.color = Color.white;
            yield break;
        }
    }
}
