using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private readonly static WaitForSeconds wait = new WaitForSeconds(0.1f);

    [SerializeField] private EnemyData enemyData;

    private Player playerLogic;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float playerHitTime = 0.2f; // 적 한명에게 맞고 그 다음 맞을 때 시간
    private float curHp;
    private float BigBulletHitTime;

    public GameManager gm;
    public Text DestroyEnemyCount;
    public GameObject player;
    public GameObject canvas;
    public Image hpBar;

    private bool isHit;
    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // 오브젝트 풀에 의해 다시 활성화되면 다 초기화 해줘야 함
    private void OnEnable()    
    {
        curHp = enemyData.Hp;
        hpBar.fillAmount = (float)curHp / enemyData.Hp;
        canvas.SetActive(false);
    }
    
    private void Update()
    {
        if(!isHit)
            rb.velocity = Vector2.zero;
        Move();
    }

    private void Move()
    {
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
        if (collision.tag == "ability Multi Hit")
        {
            playerLogic = player.GetComponent<Player>();
            float abilityDamage = collision.GetComponent<Ability>().damage + playerLogic.playerDamage;

            StopCoroutine(HideHp());
            StartCoroutine(HideHp());
            canvas.SetActive(true);

            Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;

            BigBulletHitTime += Time.deltaTime;
            if (BigBulletHitTime > 0.5f)
            {
                try
                {
                    StartCoroutine(Hit(reactVec, abilityDamage));
                    BigBulletHitTime = 0f;
                }
                catch
                {
                    ObjectPool.Instance.ReturnObject(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ability")
        {
            playerLogic = player.GetComponent<Player>();
            float abilityDamage = collision.GetComponent<Ability>().damage + playerLogic.playerDamage;

            StopCoroutine(HideHp());
            StartCoroutine(HideHp());
            canvas.SetActive(true);

            Vector2 reactVec = new Vector2(transform.position.x - collision.transform.position.x,
            transform.position.y - collision.transform.position.y).normalized;

            try
            {
                StartCoroutine(Hit(reactVec, abilityDamage));
            }
            catch
            {
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "character")
        {
            playerHitTime += Time.deltaTime;
            if (playerHitTime >= 0.2f)
            {
                playerHitTime = 0.0f;
                playerLogic = player.GetComponent<Player>();
                playerLogic.PrintPlayerHp(enemyData.Damage);
            }
        }
    }

    private IEnumerator HideHp()
    {
        yield return new WaitForSeconds(10.0f);

        canvas.SetActive(false);
    }

    private IEnumerator Hit(Vector2 reactVec, float damage)
    {
        curHp -= damage;


        GameObject dmgTextObj = ObjectPool.Instance.GetObject("DmgText");
        dmgTextObj.transform.position = transform.position + Vector3.up * 3f;
        dmgTextObj.SetActive(true);
        TextMeshPro dmgTextObjText = dmgTextObj.GetComponent<TextMeshPro>();
        dmgTextObjText.text = ((int)damage).ToString();


        hpBar.fillAmount = (float)curHp / enemyData.Hp;

        if (curHp > 0)
        {
            isHit = true;
            rb.AddForce(reactVec * 5f, ForceMode2D.Impulse);
            spriteRenderer.color = Color.red;
            yield return wait;
            spriteRenderer.color = Color.white;
            isHit = false;

        }
        else
        {
            var obj = ObjectPool.Instance.GetObject("RedSoul");
            obj.transform.position = gameObject.transform.position;
            obj.SetActive(true);

            if (transform.gameObject.name == "Monster_Boss1")
            {
                for (int i = 0; i < 5; i++)
                {
                    obj = ObjectPool.Instance.GetObject("RedSoul");
                    obj.transform.position = gameObject.transform.position;
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
