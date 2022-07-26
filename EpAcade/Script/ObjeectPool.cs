using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeectPool : MonoBehaviour
{
    public GameObject bulletPrefab;


    GameObject prefab;

    List<GameObject> Pool;

    public List<GameObject> BulletPool { get; private set; }

    private void Awake()
    {
        Pool = new List<GameObject>();

        BulletPool = new List<GameObject>();

        for(var i = 0; i < 100; i++)
        {
            var obj = Instantiate(bulletPrefab, transform);
            obj.SetActive(false);
            BulletPool.Add(obj);
        }

    }


    public GameObject GetObject(string name)
    {
        switch (name)
        {
            case "Bullet":
                Pool = BulletPool;
                prefab = bulletPrefab;
                break;


        }

        // 비활성화된 오브젝트가 없을 경우, 풀을 확장한다.
        var newObj = Instantiate(prefab, transform);
        Pool.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
