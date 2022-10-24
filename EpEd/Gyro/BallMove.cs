using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMove : MonoBehaviour
{
	Rigidbody rb;
	Vector3 dir;
	float moveSpeed = 10f;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (transform.position.y < 0)
		{
			SceneManager.LoadScene("TitleScene");
		}

		dir = new Vector3(Input.acceleration.x * moveSpeed, -5, Input.acceleration.y * moveSpeed);
	}

	private void FixedUpdate()
	{
		rb.velocity = dir;
	}

    private void OnColl (Collision collision)
    {
        switch (collision.gameObject.tag)
        {
			case "GyroCoin":
				collision.gameObject.SetActive(false);
				break;
			case "GyroGoal":
				GameManager.instance.State[3] = GameManager.instance.selectLevel++;
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				break;
			case "GyroPump":
				ContactPoint cp = collision.GetContact(0);
				Vector3 dire = transform.position - cp.point;
				rb.AddForce((dire).normalized * 300f);
				break;
			default:
				break;
		}
	}

    private void OnTriggerEnter(Collider collider)
    {
		switch (collider.gameObject.tag)
		{
			case "GyroCoin":
				collider.gameObject.SetActive(false);
				break;

			case "GyroGoal":				
				GameManager.instance.State[3] = GameManager.instance.selectLevel++;

				if (GameManager.instance.selectLevel > 10)
					SceneManager.LoadScene("TitleScene");

				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				break;
			case "GyroPump":
				//펌프 작동 안함
				ContactPoint cp = collider.GetComponent<Collision>().GetContact(0);
				Vector3 dire = transform.position - cp.point;
				rb.AddForce((dire).normalized * 300f);
				break;
			default:
				break;
		}
	}
}
