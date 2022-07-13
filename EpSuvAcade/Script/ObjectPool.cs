using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public int MonsterPoolCount = 0;

    // 추가시 ObjectPool, GameManager, EnemeyController 확인

    // 능력
    public GameObject basicAttackPrefab;
    public GameObject bigBulletPrefab;

    // 적
    public GameObject monster_1Prefab; // 풀링할 오브젝트의 프리팹
    public GameObject monster_2Prefab;
    public GameObject boss_1Prefab;

    // 효과
    public GameObject dmgTextPrefab;

    // 아이템
    public GameObject redSoulPrefab;
    public GameObject blueSoulPrefab;

    GameObject prefab;

    // 관리되는 오브젝트의 리스트

    List<GameObject> Pool;

    // 능력
    public List<GameObject> BasicAttackPool { get; private set; }
    public List<GameObject> BigBulletPool { get; private set; }

    // 적
    public List<GameObject> Monster_1 { get; private set; }
    public List<GameObject> Monster_2 { get; private set; }
    
    public List<GameObject> Boss_1 { get; private set; }

    // 효과
    public List<GameObject> DmgTextPool { get; private set; }

    // 아이템
    public List<GameObject> RedSoulPool { get; private set; }
    public List<GameObject> BlueSoulPool { get; private set; }


    private void Awake()
    {
        Instance = this;

        Pool = new List<GameObject>();

        BasicAttackPool = new List<GameObject>();
        BigBulletPool = new List<GameObject>();

        Monster_1 = new List<GameObject>();
        Monster_2 = new List<GameObject>();
        Boss_1 = new List<GameObject>();

        RedSoulPool = new List<GameObject>();
        BlueSoulPool = new List<GameObject>();

        DmgTextPool = new List<GameObject>();


        for (var i = 0; i < 3; i++)
        {
            var obj = Instantiate(basicAttackPrefab, transform);
            obj.SetActive(false);
            BasicAttackPool.Add(obj);
        }
        for (var i = 0; i < 3; i++)
        {
            var obj = Instantiate(bigBulletPrefab, transform);
            obj.SetActive(false);
            BigBulletPool.Add(obj);
        }


        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(monster_1Prefab, transform);
            obj.SetActive(false);
            Monster_1.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(monster_2Prefab, transform);
            obj.SetActive(false);
            Monster_1.Add(obj);
        }
        for (var i = 0; i < 20; i++)
        {
            var obj = Instantiate(boss_1Prefab, transform);
            obj.SetActive(false);
            Boss_1.Add(obj);
        }

        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(dmgTextPrefab, transform);
            obj.SetActive(false);
            DmgTextPool.Add(obj);
        }

        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(redSoulPrefab, transform);
            obj.SetActive(false);
            RedSoulPool.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(blueSoulPrefab, transform);
            obj.SetActive(false);
            BlueSoulPool.Add(obj);
        }
    }

    // 오브젝트 풀에서 관리하는 오브젝트를 반환한다.
    public GameObject GetObject(string name)
    {
        switch (name)
        {
            case "BasicAttack":
                Pool = BasicAttackPool;
                prefab = basicAttackPrefab;
                break;
            case "BigBullet":
                Pool = BigBulletPool;
                prefab = bigBulletPrefab;
                break;

            //------------------------------------------------------------------

            case "Skeleton":
                Pool = Monster_1;
                prefab = monster_1Prefab;
                MonsterPoolCount++;
                break;
            case "Goblin":
                Pool = Monster_2;
                prefab = monster_2Prefab;
                MonsterPoolCount++;
                break;
            case "Boss1":
                Pool = Boss_1;
                prefab = boss_1Prefab;
                MonsterPoolCount++;
                break;
            //------------------------------------------------------------------
            case "DmgText":
                Pool = DmgTextPool;
                prefab = dmgTextPrefab;
                break;

            //------------------------------------------------------------------
            case "RedSoul":
                Pool = RedSoulPool;
                prefab = redSoulPrefab;
                break;
            case "BlueSoul":
                Pool = BlueSoulPool;
                prefab = blueSoulPrefab;
                break;
        }

        // 풀에서 비활성화된 오브젝트를 찾아 반환한다.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }
        
        //Debug.Log(MonsterPoolCount);

        // 비활성화된 오브젝트가 없을 경우, 풀을 확장한다.
        var newObj = Instantiate(prefab, transform);
        Pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj.layer == 6)
        {
            MonsterPoolCount--;
        }

        obj.SetActive(false);
    }

    public void AllReturnObject()
    {
        int iCount = transform.childCount;
        for(int i = 0; i < iCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeSelf) // 활성화되어 있으면 전부 비활성화
            {
                child.SetActive(false);
            }
        }
    }
}
