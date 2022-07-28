using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static readonly WaitForFixedUpdate wait = new WaitForFixedUpdate();
        
    private static GameManager instance;

    #region Game
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

    public Shooter shooter;
    #endregion

    #region Camera
    // 카메라 효과 관련
    private static Camera mainCamera;
    private static Vector3 defPos;
    private static Vector3 initialPosition;

    public static float ShakeAmount = 0.05f;
    public static float ShakeTime = 0.2f;
    #endregion

    public static int CheckBlock = -1;

    public Boss boss;
    public static bool bossDown = false;

    // 활성화시
    private void OnEnable()
    {
        Application.targetFrameRate = 60;

        InitGame();
    }

    private void Update()
    {
        // 게임 시작전 또는 게임 오버시
        if (!isGameStart || isGameOver)
            return;

        // 보스 다운
        if (bossDown)
        {
            bossDown = false;
            score += instance.boss.Level * 1000;
        }

        GameTimer();
    }

    // 초기화
    public void InitGame()
    {
        instance = this;

        boss.SetMaxHP(100);

        hit = 0;
        combo = 0;
        score = 0;
        gameTime = 30;
        playTime = 0;
        absTime = 0;

        playComboTime = 0;

        mainCamera = Camera.main;
        defPos = mainCamera.transform.position;

        isGameOver = false;
        comboSet = false;

        // 게임 시작시 사용자 입력 받을때까지 멈춤
        if (SceneManager.GetActiveScene().name != "Main")
        {
            if (isGameStart)
                isGameStart = false;

            return;
        }

        instance.StartCoroutine(instance.ComboSt());

        // 2키 모드인지 4키 모드인지 판단
        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(TouchWait());
            
            // 키 활성화 유무
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
        comboSet = true;    // 콤보 유지

        hit++;
        combo++;

        float bonus = 0;

        if (btnNum)
            bonus = 10f;

        score += 1 + hit + (int)(combo * hit * 0.02f * bonus);

        if(playTime > 1)
            playTime -= 1f;

        playComboTime = 0;

        // 탄 발사
        instance.shooter.Shot();

        // static 호출
        //instance.StartCoroutine(instance.offCombo());
    }

    // 실패
    public static void Fail()
    {
        // 플레이 타임 감소
        if (playTime < gameTime)
            playTime += 1f;

        // 카메라 흔들림
        ShakeTime = 0.2f;
        instance.StartCoroutine(CameraMove());
       
        // 콤보 초기화
        comboSet = false;
        combo = 0;
    }

    // 보스 피격
    public static void BossHit()
    {
        instance.boss.Hit(1f);
    }

    // 콤보 유지 확인
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

    // 게임 타이머
    private void GameTimer()
    {
        absTime += Time.deltaTime;
        playTime += Time.deltaTime + (absTime * 0.001f);

        if (playTime > gameTime)
            GameOver();
    }

    // 게임 오버
    private void GameOver()
    {
        isGameOver = true;

        UI.SetActive(false);
        UIGameOver.SetActive(true);

        StartCoroutine(UIMov());
    }

    // 게임 오버시 UI 출력
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

    // 카메라 무빙
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

    // 터치 입력 대기
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
