using UnityEngine;
using System.Collections;
using System;

public class CameraLookAt : MonoBehaviour
{
	public Transform Target;

	private void Update ()
	{
		transform.LookAt (Target);
	}
}
