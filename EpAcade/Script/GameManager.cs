using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly WaitForFixedUpdate wait = new WaitForFixedUpdate();
        
    static GameManager instance;

    // 게임 시간
    public static float gameTime = 30f;     // 게임 한 판 시간
    public static float playTime = 0;       // 플레이 시간
    public static float absTime = 0;        // 시간 가속용 절대 시간
    
    // 콤보 시간
    public static float maxComboTime = 1.5f;// 콤보 최대 유지시간
    public static float playComboTime;      // 콤보 체크시간

    // 점수 관련
    public static float score;
    public static int hit;
    public static int combo;


    // 게임 관련
    public static int maxBlock = 4;
    public static int textShadowCount;

    public static bool isGameStart;
    public static bool isGameOver;
    public static bool comboSet;

    public GameObject UI;
    public GameObject UIGameOver;

    public static bool btnNum;
    public GameObject[] Button;

    // 카메라 효과 관련
    private static Camera mainCamera;
    private static Vector3 defPos;
    private static Vector3 initialPosition;
    public static float ShakeAmount = 0.05f;
    public static float ShakeTime = 0.2f;
    
    
    private void OnEnable()
    {
        Application.targetFrameRate = 60;

        InitGame();
    }

    public void InitGame()
    {

        hit = 0;
        combo = 0;
        score = 0;
        gameTime = 30;
        playTime = 0;
        absTime = 0;

        playComboTime = 0;

        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (isGameStart)
                isGameStart = false;

            return;
        }



        instance = this;

        mainCamera = Camera.main;
        defPos = mainCamera.transform.position;

        isGameOver = false;
        comboSet = false;

        instance.StartCoroutine(instance.ComboSt());

        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(TouchWait());
            
            if (!btnNum)
            {

                maxBlock = 2;

                Button[0].SetActive(false);
                Button[1].SetActive(false);
            }
            else
            {
                maxBlock = 4;

                Button[0].SetActive(true);
                Button[1].SetActive(true);

            }
        }
    }

    // 성공
    public static void Success()
    {
        comboSet = true;

        hit++;
        combo++;

        float bonus = 0;

        if (btnNum)
            bonus = 10f;

        score += 1 + hit + (int)(combo * hit * 0.02f * bonus);

        if(playTime > 1)
            playTime -= 1f;

        playComboTime = 0;

        // static 호출
        //instance.StartCoroutine(instance.offCombo());
    }

    // 실패
    public static void Fail()
    {
        if (playTime < gameTime)
            playTime += 1f;

        ShakeTime = 0.2f;
        instance.StartCoroutine(CameraMove());
       
        comboSet = false;
        combo = 0;
    }

    private IEnumerator ComboSt()
    {
        while (!isGameOver)
        {
            yield return null;

            playComboTime += Time.deltaTime;

            if (playComboTime > maxComboTime)
                combo = 0;
        }
    }

    private void Update()
    {
        if (!isGameStart || isGameOver)
            return;

        GameTimer();
    }

    private void GameTimer()
    {
        absTime += Time.deltaTime;
        playTime += Time.deltaTime + (absTime * 0.001f);

        if (playTime > gameTime)
            GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;

        UI.SetActive(false);
        UIGameOver.SetActive(true);

        StartCoroutine(UIMov());
    }

    private IEnumerator UIMov()
    {
        Text text = UIGameOver.GetComponentInChildren<Text>();
        GameObject button = GameObject.Find("Button");


        button.SetActive(false);
        iTween.MoveTo(text.gameObject, iTween.Hash("y", text.gameObject.transform.position.y / 2, "Time", 3f, "easeType", iTween.EaseType.easeOutBounce));

        yield return new WaitForSeconds(3f);

        iTween.MoveTo(text.gameObject, iTween.Hash("y", -text.gameObject.transform.position.y / 2, "Time", 2f, "easeType", iTween.EaseType.easeOutBounce));

        yield return new WaitForSeconds(3f);

        button.SetActive(true);

    }

    private static IEnumerator CameraMove()
    {
        while(true)
        {
            initialPosition = mainCamera.gameObject.transform.position;     //카메라 흔들릴 위치값

            if (ShakeTime > 0)
            {
                mainCamera.gameObject.transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
                ShakeTime -= Time.deltaTime;
            }
            else
            {
                ShakeTime = 0.0f;
                mainCamera.gameObject.transform.position = defPos;
                yield break;
            }

            yield return wait;
        }
    }

    private static IEnumerator TouchWait()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                isGameStart = true;
                yield break;
            }

            yield return wait;
        }

    }
}
