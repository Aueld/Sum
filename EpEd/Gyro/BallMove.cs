using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
	Rigidbody rb;
	Vector3 dir;
	float moveSpeed = 10f;

	private Vector3 startPos;

	void Start()
	{
		startPos = transform.position;
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if(transform.position.y < 0)
			transform.position = startPos;

		dir = new Vector3(Input.acceleration.x * moveSpeed, 0 , Input.acceleration.y * moveSpeed);

		//transform.position =
		//	new Vector3(
		//		Mathf.Clamp(transform.position.x, -7.5f, 7.5f),
		//		transform.position.y ,
		//		Mathf.Clamp(transform.position.z, -7.5f, 7.5f)
		//	);
	}

	void FixedUpdate()
	{
		rb.velocity = dir;
	}

}
