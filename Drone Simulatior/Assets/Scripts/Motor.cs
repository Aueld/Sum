using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour
{
    public float UpForce = 0.0f;		// 모터에 적용되는 종력
    public float SideForce = 0.0f;		// 모터에 적용되는 횡력
	public float Power = 1.5f;			// 모터에 적용되는 총 힘
    public float ExceedForce = 0.0f;	// 종력이 0미만일때 음수

	public float YawFactor = 0.0f;		// 횡력 적용 인자. 값이 높을수록 Yaw 움직임이 빨라집니다.
    public bool InvertDirection;		// 모터의 방향 여부. 시계 방향, 반시계 방향
	public float PitchFactor = 0.0f;	// Pitch 보정 인자
	public float RollFactor = 0.0f;		// Roll 보정 인자

    public float Mass = 0.0f;			// 질량. 무게

	public BasicControl mainController; // 상위 메인 컨트롤러
	public GameObject Propeller;		// 프로펠러. 애니메이션 수행
	private float SpeedPropeller = 0;	// 프로펠러 회전 속도

	// 특정 모터의 힘 값 계산
    public void UpdateForceValues()
	{
        float UpForceThrottle = Mathf.Clamp(mainController.ThrottleValue, 0, 1) * Power;
        float UpForceTotal = UpForceThrottle;


		// 이동 계산
		UpForceTotal -= mainController.Computer.PitchCorrection * PitchFactor;
		UpForceTotal -= mainController.Computer.RollCorrection * RollFactor;

		UpForce = UpForceTotal;
		SideForce = PreNormalize (mainController.Controller.Yaw, YawFactor);

		// 프로펠러 계산
        SpeedPropeller = Mathf.Lerp(SpeedPropeller, UpForce * 2000.0f, Time.deltaTime);
        UpdatePropeller(SpeedPropeller);
	}

	// 드론 프로펠러 회전 값
    public void UpdatePropeller(float speed)
    {
        Propeller.transform.Rotate(0.0f, SpeedPropeller * speed * Time.deltaTime, 0.0f);
    }

	// 벡터 사전 정규화
	private float PreNormalize(float input, float factor) {
		float finalValue = input;

		if (InvertDirection)
			finalValue = Mathf.Clamp (finalValue, -1, 0);
		else
			finalValue = Mathf.Clamp (finalValue, 0, 1);

		return finalValue * factor;
	}
}
