using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRoll : MonoBehaviour
{
    private float rotSpeed = 20f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
