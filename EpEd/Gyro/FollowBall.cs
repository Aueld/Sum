using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    [SerializeField] private Transform ball;

    private Vector3 vecXZ;

    private void Update()
    {
        vecXZ = new Vector3(0, 10, ball.position.z);

        transform.position = vecXZ;
    }
}
