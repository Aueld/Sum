using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
	Rigidbody rb;
	Vector3 dir;
	float moveSpeed = 10f;

	private Vector3 startPos;

	private void Start()
	{
		startPos = transform.position;
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if(transform.position.y < 0)
			transform.position = startPos;

		dir = new Vector3(Input.acceleration.x * moveSpeed, 0 , Input.acceleration.y * moveSpeed);
	}

	private void FixedUpdate()
	{
		rb.velocity = dir;
	}

}
