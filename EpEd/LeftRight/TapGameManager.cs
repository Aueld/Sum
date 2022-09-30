using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TapGameManager : MonoBehaviour
{
    public static readonly WaitForFixedUpdate wait = new WaitForFixedUpdate();
        
    private static TapGameManager instance;

    #region Game
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

    public static int goalCount;

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

    // 활성화시
    private void OnEnable()
    {
        InitGame();
    }

    private void Update()
    {
        // 게임 시작전 또는 게임 오버시
        if (isGameOver)
            GameOver();
    }

    // 초기화
    public void InitGame()
    {
        instance = this;

        hit = 0;
        combo = 0;
        score = 0;
        playComboTime = 0;

        goalCount = 5 + GameManager.instance.selectLevel * 5;

        mainCamera = Camera.main;
        defPos = mainCamera.transform.position;

        isGameOver = false;
        comboSet = false;

        instance.StartCoroutine(instance.ComboSt());
    }

    // 성공
    public static void Success()
    {
        comboSet = true;    // 콤보 유지

        hit++;
        combo++;

        if (hit > goalCount)
            isGameOver = true;

        score += 1 + hit + (int)(combo * hit * 0.02f);

        playComboTime = 0;

    }

    // 실패
    public static void Fail()
    {
        // 카메라 흔들림
        ShakeTime = 0.2f;
        instance.StartCoroutine(CameraMove());
       
        // 콤보 초기화
        comboSet = false;
        combo = 0;
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

    // 게임 오버
    private void GameOver()
    {

        Debug.Log("게임 오버");

        isGameOver = false;

        UI.SetActive(false);

        StartCoroutine(ReturnGame());

        //StartCoroutine(UIMov());
    }


    private IEnumerator ReturnGame()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("TitleScene");
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
}
