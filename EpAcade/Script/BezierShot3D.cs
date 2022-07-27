using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierShot3D : MonoBehaviour
{
    public ObjeectPool pool;

    Vector3[] point = new Vector3[4];
    Animator anim;
    bool hit = false;

    [SerializeField][Range(0, 1)] private float t = 0;
    [SerializeField] public float spd = 5;
    [SerializeField] public float posA = 0.55f;
    [SerializeField] public float posB = 0.45f;
    [SerializeField] public float posC = 0.35f;

    public GameObject master;
    public GameObject enemy;

    void Start()
    {
        anim = GetComponent<Animator>();

        Init();
    }

    private void OnEnable()
    {
        Init();


        StartCoroutine(ReturnOBJ());
    }

    void FixedUpdate()
    {
        if (t > 1) return;
        if (hit) return;
        t += Time.deltaTime * spd;
        DrawTrajectory();
    }

    private void Init()
    {
        t = 0;

        point[0] = master.transform.position; // P0
        point[1] = PointSetting(master.transform.position); // P1
        point[2] = PointSetting(enemy.transform.position); // P2
        point[3] = enemy.transform.position; // P3

        transform.position = master.transform.position;
    }

    Vector3 PointSetting(Vector3 origin)
    {
        float x, y, z;

        x = posA * Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad)
        + origin.x;
        y = posB * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad)
        + origin.y;
        z = posC * Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad)
        + origin.z;


        return new Vector3(x, y, z);
    }

    void DrawTrajectory()
    {
        transform.position = new Vector3(
        FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x),
        FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y),
        FourPointBezier(point[0].z, point[1].z, point[2].z, point[3].z)
        );
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)        
            pool.ReturnObject(gameObject);
    }


    private float FourPointBezier(float a, float b, float c, float d)
    {
        return Mathf.Pow((1 - t), 3) * a
        + Mathf.Pow((1 - t), 2) * 3 * t * b
        + Mathf.Pow(t, 2) * 3 * (1 - t) * c
        + Mathf.Pow(t, 3) * d;
    }

    private IEnumerator ReturnOBJ()
    {
        yield return new WaitForSeconds(3f);

        if(gameObject.activeSelf)
            pool.ReturnObject(gameObject);
    }
}
