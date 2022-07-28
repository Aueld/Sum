using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] public GameObject target;

    [SerializeField] public ObjeectPool pool;

    [SerializeField] public float spd;
    [SerializeField] public int shot = 12;

    public void Shot()
    {
        StartCoroutine(CreateMissile());
    }

    private IEnumerator CreateMissile()
    {
        int _shot = shot;
        while (_shot > 0)
        {
            _shot--;
            GameObject bullet = pool.GetObject("Bullet");
            
            bullet.GetComponent<BezierShot3D>().master = gameObject;
            bullet.GetComponent<BezierShot3D>().enemy = target;
            bullet.GetComponent<BezierShot3D>().pool = pool;

            bullet.SetActive(true);

            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
