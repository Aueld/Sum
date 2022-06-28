using UnityEngine;
using System.Collections;

public class ComputerModule : MonoBehaviour
{
    [Range(0, 360)] public float PitchLimit;
    [Range(0, 360)] public float RollLimit;

    public PID PidThrottle;
    public PID PidPitch;
    public PID PidRoll;
    public BasicGyro Gyro;

    public float PitchCorrection;
    public float RollCorrection;
    public float HeightCorrection;

    public void UpdateComputer(float ControlPitch, float ControlRoll, float ControlHeight)
    {
        UpdateGyro();

        //PitchCorrection = PidPitch.Update(ControlPitch * PitchLimit, Gyro.Pitch, Time.deltaTime);
        //RollCorrection = PidRoll.Update(Gyro.Roll, ControlRoll * RollLimit, Time.deltaTime);

        if (PitchCorrection > 180 || PitchCorrection < -180)
        {
            GetComponent<BasicControl>().ThrottleValue = 0f;
            Gyro.Pitch = 0;
            PitchCorrection = 0;
        }
        else
            PitchCorrection = PidPitch.Update(ControlPitch * PitchLimit, Gyro.Pitch, Time.deltaTime);

        if (RollCorrection > 180 || RollCorrection < -180)
        {
            GetComponent<BasicControl>().ThrottleValue = 0f;
            Gyro.Roll = 0;
            RollCorrection = 0;
        }
        else
            RollCorrection = PidRoll.Update(Gyro.Roll, ControlRoll * RollLimit, Time.deltaTime);

        HeightCorrection = PidThrottle.Update(ControlHeight, Gyro.VelocityVector.y, Time.deltaTime);
    }

    public void UpdateGyro()
    {
        Gyro.UpdateGyro(transform);
    }
}
