using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // 능력
    public GameObject basicAttackPrefab;
    public GameObject bigBulletPrefab;

    // 적
    public GameObject monsterPrefab; // 풀링할 오브젝트의 프리팹

    // 효과
    public GameObject dmgTextPrefab;

    // 아이템
    public GameObject redSoulPrefab;
    public GameObject blueSoulPrefab;

    GameObject prefab;

    // 관리되는 오브젝트의 리스트

    List<GameObject> Pool;

    // 능력
    public List<GameObject> basicAttackPool { get; private set; }
    public List<GameObject> bigBulletPool { get; private set; }

    // 적
    public List<GameObject> monster { get; private set; } 

    // 효과
    public List<GameObject> dmgTextPool { get; private set; }

    // 아이템
    public List<GameObject> redSoulPool { get; private set; }
    public List<GameObject> blueSoulPool { get; private set; }


    private void Awake()
    {
        Instance = this;

        Pool = new List<GameObject>();

        basicAttackPool = new List<GameObject>();
        bigBulletPool = new List<GameObject>();

        monster = new List<GameObject>();

        redSoulPool = new List<GameObject>();
        blueSoulPool = new List<GameObject>();

        dmgTextPool = new List<GameObject>();


        for (var i = 0; i < 3; i++)
        {
            var obj = Instantiate(basicAttackPrefab, transform);
            obj.SetActive(false);
            basicAttackPool.Add(obj);
        }
        for (var i = 0; i < 3; i++)
        {
            var obj = Instantiate(bigBulletPrefab, transform);
            obj.SetActive(false);
            bigBulletPool.Add(obj);
        }


        for (var i = 0; i < 50; i++)
        {
            var obj = Instantiate(monsterPrefab, transform);
            obj.SetActive(false);
            monster.Add(obj);
        }

        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(dmgTextPrefab, transform);
            obj.SetActive(false);
            dmgTextPool.Add(obj);
        }

        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(redSoulPrefab, transform);
            obj.SetActive(false);
            redSoulPool.Add(obj);
        }
        for (var i = 0; i < 100; i++)
        {
            var obj = Instantiate(blueSoulPrefab, transform);
            obj.SetActive(false);
            blueSoulPool.Add(obj);
        }
    }

    // 오브젝트 풀에서 관리하는 오브젝트를 반환한다.
    public GameObject GetObject(string name)
    {
        switch (name)
        {
            case "BasicAttack":
                Pool = basicAttackPool;
                prefab = basicAttackPrefab;
                break;
            case "BigBullet":
                Pool = bigBulletPool;
                prefab = bigBulletPrefab;
                break;


            case "Skeleton":
                Pool = monster;
                prefab = monsterPrefab;
                break;

            case "DmgText":
                Pool = dmgTextPool;
                prefab = dmgTextPrefab;
                break;

            case "RedSoul":
                Pool = redSoulPool;
                prefab = redSoulPrefab;
                break;
            case "BlueSoul":
                Pool = blueSoulPool;
                prefab = blueSoulPrefab;
                break;
        }

        // 풀에서 비활성화된 오브젝트를 찾아 반환한다.
        foreach (var obj in Pool)
            if (!obj.activeSelf)
            {
                return obj;
            }

        // 비활성화된 오브젝트가 없을 경우, 풀을 확장한다.
        var newObj = Instantiate(prefab, transform);
        Pool.Add(newObj);
        //poolCount++;
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
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
