using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasicGyro
{
	public float Pitch;
	public float Roll;
	public float Yaw;
    public float Altitude;

    public Vector3 VelocityVector;
    public float VelocityScalar;

	public bool stop = false;

	// 물체 물리 처리
	public void UpdateGyro(Transform transform)
	{
		if (stop)
			return;

        Pitch = transform.eulerAngles.x;
		Pitch = (Pitch > 180) ? Pitch - 360 : Pitch;

		Roll = transform.eulerAngles.z;
		Roll = (Roll > 180) ? Roll - 360 : Roll;

		Yaw = transform.eulerAngles.y;
		//Yaw = (Yaw > 180) ? Yaw - 360 : Yaw;

		Altitude = transform.position.y;

		VelocityVector = transform.GetComponent<Rigidbody>().velocity;
		VelocityScalar = VelocityVector.magnitude;
	}
}
