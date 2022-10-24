using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinballCameraMove : MonoBehaviour
{
    [SerializeField] private GameObject spin;

    private Vector3 move;

    private void Update()
    {
        move = spin.transform.position;

        transform.position = new Vector3(0, 24, move.z + 3f);
    }
}
