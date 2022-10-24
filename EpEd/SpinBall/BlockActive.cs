using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockActive : MonoBehaviour
{
    Transform spinball;

    private void Start()
    {
        spinball = GameObject.Find("Spin").transform;
    }

    void Update()
    {
        if (transform.position.z < spinball.position.z - 10)
        {
            gameObject.SetActive(false);
        }
    }
}
