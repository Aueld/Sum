using UnityEngine;
using System.Collections;

public class BasicControl : MonoBehaviour
{
    // 컨트롤
	public Controller Controller;
	public float ThrottleIncrease;
	
	// 모터
    public Motor[] Motors;
	public float ThrottleValue;

    // 연산 컴퓨터
    public ComputerModule Computer;

	private void FixedUpdate()
    {
        Computer.UpdateComputer(Controller.Pitch, Controller.Roll, Controller.Throttle * ThrottleIncrease);
        ThrottleValue = Computer.HeightCorrection;
        
        ComputeMotors();

        if (Computer != null)
            Computer.UpdateGyro();
        
        ComputeMotorSpeeds();
	}

    // 모터 이동 계산
    private void ComputeMotors()
    {
        float yaw = 0.0f;
        Rigidbody rb = GetComponent<Rigidbody>();
        int i = 0;

        foreach (Motor motor in Motors)
        {
            motor.UpdateForceValues();
            yaw += motor.SideForce;
            i++;
            Transform t = motor.GetComponent<Transform>();

            rb.AddForceAtPosition(transform.up * motor.UpForce, t.position, ForceMode.Impulse);
        }
        
        rb.AddTorque(Vector3.up * yaw, ForceMode.Force);
    }

    // 모터 스피드 계산
    private void ComputeMotorSpeeds()
    {
        foreach (Motor motor in Motors)
        {
            if (Computer.Gyro.Altitude < 0.1)
                motor.UpdatePropeller(0.0f);
            else
                motor.UpdatePropeller(10.0f);
        }
    }
}