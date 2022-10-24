using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    [SerializeField] private Transform Board;

    private Transform ball;
    private Vector3 vecXZ;

    private void Start()
    {
        ball = Board.GetComponentInChildren<BallMove>().gameObject.transform;
    }

    private void Update()
    {
        vecXZ = new Vector3(ball.position.x, 24, ball.position.z);

        transform.position = vecXZ;
    }
}
