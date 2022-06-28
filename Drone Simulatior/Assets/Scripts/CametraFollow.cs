using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CametraFollow : MonoBehaviour
{
    public GameObject target;

    private Quaternion rot;
    private float angle;

    private void Update()
    {
        angle = Mathf.LerpAngle(transform.eulerAngles.y, target.transform.eulerAngles.y, 5.0f * Time.deltaTime);
        
        rot = Quaternion.Euler(0, angle, 0);
        
        transform.position = target.transform.position - (rot * Vector3.forward * 10f) + (Vector3.up * 5f);

        transform.LookAt(target.transform);
    }
}
