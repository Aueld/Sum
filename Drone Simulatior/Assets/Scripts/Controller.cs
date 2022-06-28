using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	public float Throttle = 0.0f;
	public float Yaw = 0.0f;
	public float Pitch = 0.0f;
	public float Roll = 0.0f;

	private float Power = 0.0f;

    public enum ThrottleMode { None, LockHeight};

	// 위, 아래
	public string ThrottleCommand = "Throttle";
	public bool InvertThrottle = true;

	// 회전
	public string YawCommand = "Yaw";
	public bool InvertYaw = false;

	// 전진 이동, 후진 이동
	public string PitchCommand = "Pitch";
	public bool InvertPitch = true;

	// 좌측 이동, 우측 이동
	public string RollCommand = "Roll";
	public bool InvertRoll = true;

    private void Start()
    {
		Power = 1f;
    }

	// 물리 입력 처리
	private void Update()
	{
		Throttle = Input.GetAxisRaw(ThrottleCommand) * (InvertThrottle ? Power : Power);
		Yaw = Input.GetAxisRaw(YawCommand) * (InvertYaw ? -Power : Power);


		Pitch = Input.GetAxisRaw(PitchCommand) * (InvertPitch ? -Power : Power);
		Roll = Input.GetAxisRaw(RollCommand) * (InvertRoll ? -Power : Power);

	}
}
