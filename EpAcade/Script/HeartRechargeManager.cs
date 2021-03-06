using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeartRechargeManager : MonoBehaviour
{
    #region Heart
    //화면에 표시하기 위한 UI변수
    //public Text appQuitTimeLabel = null;
    public Text heartRechargeTimer = null;
    public Text heartAmountLabel = null;
    
    private int m_HeartAmount = 0; //보유 하트 개수
    private DateTime m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();
    private const int MAX_HEART = 5; //하트 최대값
    
    private int HeartRechargeInterval = 30;// 하트 충전 간격(단위:초)
    
    private Coroutine m_RechargeTimerCoroutine = null;
    private int m_RechargeRemainTime = 0;
    #endregion

    private void Awake()
    {
        Init();

        LoadHeartInfo();
        LoadAppQuitTime();

        SetRechargeScheduler();
    }

    // 게임 초기화, 중간 이탈, 중간 복귀 시 실행되는 함수
    public void OnApplicationFocus(bool value)
    {
        if (value)
        {
            LoadHeartInfo();
            LoadAppQuitTime();

            SetRechargeScheduler();
        }
    }

    // 게임 이탈시 실행되는 함수
    private void OnApplicationQuit()
    {
        SaveHeartInfo();
        SaveAppQuitTime();
    }

    // 버튼 이벤트에 이 함수를 연동
    public void OnClickUseHeart()
    {
        UseHeart();

        SaveHeartInfo();
        SaveAppQuitTime();
    }

    // 초기화
    public void Init()
    {
        m_HeartAmount = 0;
        m_RechargeRemainTime = 0;
        m_AppQuitTime = new DateTime(1970, 1, 1).ToLocalTime();

        heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);
    }

    // 데이터 로드
    public bool LoadHeartInfo()
    {
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("HeartAmount"))
            {
                Debug.Log("PlayerPrefs has key : HeartAmount");
                m_HeartAmount = PlayerPrefs.GetInt("HeartAmount");
                if (m_HeartAmount < 0)
                {
                    m_HeartAmount = 0;
                }
            }
            else
            {
                m_HeartAmount = MAX_HEART;
            }
            heartAmountLabel.text = m_HeartAmount.ToString();
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }

    public bool SaveHeartInfo()
    {
        bool result = false;
        try
        {
            PlayerPrefs.SetInt("HeartAmount", m_HeartAmount);
            PlayerPrefs.Save();
            Debug.Log("Saved HeartAmount : " + m_HeartAmount);
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveHeartInfo Failed (" + e.Message + ")");
        }
        return result;
    }

    public bool LoadAppQuitTime()
    {
        bool result = false;
        try
        {
            if (PlayerPrefs.HasKey("AppQuitTime"))
            {
                Debug.Log("PlayerPrefs has key : AppQuitTime");
                var appQuitTime = string.Empty;
                appQuitTime = PlayerPrefs.GetString("AppQuitTime");
                m_AppQuitTime = DateTime.FromBinary(Convert.ToInt64(appQuitTime));
            }
            
            Debug.Log(string.Format("Loaded AppQuitTime : {0}", m_AppQuitTime.ToString()));

            //appQuitTimeLabel.text = string.Format("AppQuitTime : {0}", m_AppQuitTime.ToString());
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("LoadAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }

    public bool SaveAppQuitTime()
    {
        bool result = false;
        try
        {
            var appQuitTime = DateTime.Now.ToLocalTime().ToBinary().ToString();
            PlayerPrefs.SetString("AppQuitTime", appQuitTime);
            PlayerPrefs.Save();

            Debug.Log("Saved AppQuitTime : " + DateTime.Now.ToLocalTime().ToString());
            
            result = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("SaveAppQuitTime Failed (" + e.Message + ")");
        }
        return result;
    }
    public void SetRechargeScheduler(Action onFinish = null)
    {
        if (m_RechargeTimerCoroutine != null)
        {
            StopCoroutine(m_RechargeTimerCoroutine);
        }
        var timeDifferenceInSec = (int)((DateTime.Now.ToLocalTime() - m_AppQuitTime).TotalSeconds);
        
        Debug.Log(timeDifferenceInSec + " WHHHHHA");

        var heartToAdd = timeDifferenceInSec / HeartRechargeInterval;

        var remainTime = HeartRechargeInterval - timeDifferenceInSec % HeartRechargeInterval;

        m_HeartAmount += heartToAdd;

        if (m_HeartAmount >= MAX_HEART)
        {
            m_HeartAmount = MAX_HEART;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(remainTime, onFinish));
        }

        heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
    }

    public void UseHeart(Action onFinish = null)
    {
        if (m_HeartAmount <= 0)
        {
            return;
        }

        m_HeartAmount--;
        heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
        if (m_RechargeTimerCoroutine == null)
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval));
        }
        if (onFinish != null)
        {
            onFinish();
        }
    }
    private IEnumerator DoRechargeTimer(int remainTime, Action onFinish = null)
    {
        if (remainTime <= 0)
        {
            m_RechargeRemainTime = HeartRechargeInterval;
        }
        else
        {
            m_RechargeRemainTime = remainTime;
        }

        while (m_RechargeRemainTime > 0)
        {
            heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);
        
            m_RechargeRemainTime -= 1;

            yield return new WaitForSeconds(1f);
        }

        m_HeartAmount++;

        if (m_HeartAmount >= MAX_HEART)
        {
            m_HeartAmount = MAX_HEART;
            m_RechargeRemainTime = 0;
            heartRechargeTimer.text = string.Format("Timer : {0} s", m_RechargeRemainTime);

            m_RechargeTimerCoroutine = null;
        }
        else
        {
            m_RechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(HeartRechargeInterval, onFinish));
        }

        heartAmountLabel.text = string.Format("Hearts : {0}", m_HeartAmount.ToString());
    }
}