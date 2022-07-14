using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject gameOverPanel;
    public Controller controller;

    public float playerDamage = 5.0f;
    public float speed = 10.0f;
    public float maxHp = 100;
    public float curHp;
    public Image hpImage;

    private Animator anim;

    [SerializeField] private GameObject Character;

    private bool isMove;
    private Vector2 movement;

    /// <summary>
    /// 능력
    /// </summary>

    // 능력 프리팹
    // 능력 획득 유무
    // 능력 유지 시간

    // 기본 공격
    private float basicTIme;
    public Basic basicAbility;
    public bool hasAbilityBasic;
    
    // 대형 환탄
    private float BigBulletTime;
    public BigBullet bigBUlletAbility;
    public bool hasAbilityBigBullet;
    

    private void Start()
    {
        curHp = maxHp;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Move();
        Attack();
    }
    
    private void Move()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        // 하드웨어 조작
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else
        //  터치스크린 조작
            movement = controller.vecJoystickValue;
                
        isMove = movement.magnitude != 0;
        Vector2 dir = movement.normalized;

        if (isMove)
        {
            if (movement.x > 0)
            {
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if(movement.x < 0)
            {
                Character.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            transform.Translate(dir * Time.deltaTime * speed);
            anim.SetBool("isRun", true);
        }
        else { anim.SetBool("isRun", false); }
    }

    private void Attack()
    {
        if (hasAbilityBasic)
        {
            basicTIme += Time.deltaTime;
            if (basicTIme >= basicAbility.coolTime)
            {
                basicTIme = 0;

                GameObject basic = ObjectPool.Instance.GetObject("BasicAttack");
                Basic basicA = basic.GetComponent<Basic>();
                basicA.player = transform;

                basic.SetActive(true);
                basicA.Logic();
            }
        }

        if (hasAbilityBigBullet)
        {
            BigBulletTime += Time.deltaTime;
            if (BigBulletTime >= bigBUlletAbility.coolTime)
            {
                BigBulletTime = 0f;

                GameObject BigBulletO = ObjectPool.Instance.GetObject("BigBullet");
                BigBullet bigbullet = BigBulletO.GetComponent<BigBullet>();
                bigbullet.player = transform;

                BigBulletO.SetActive(true);
                bigbullet.Logic();
            }
        }
    }

    public void LevelUp(float soulAmount)
    {
        gameManager.LevelUp(soulAmount);
    }

    public void PrintPlayerHp(float damage)
    {
        curHp -= damage;

        hpImage.fillAmount = curHp / maxHp;
        if(curHp <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
