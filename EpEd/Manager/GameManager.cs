using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int selectLevel;
    public int reLayer = 0;
    public string loadScene;
    public bool[] clearCheck;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Update()
    {
        BackScene();
    }

    private void BackScene()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name != "TitleScene")
                {
                    SceneManager.LoadScene("TitleScene");
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name != "TitleScene")
                {
                    SceneManager.LoadScene("TitleScene");
                }
            }
        }
    }

    // 게임 오버시 UI 출력
    private IEnumerator GameOver(GameObject EndCanvas)
    {
        WaitForSeconds wait = new WaitForSeconds(3f);

        TextMeshProUGUI text = EndCanvas.GetComponent<TextMeshProUGUI>();


        //iTween.MoveTo(text.gameObject, iTween.Hash("y", text.gameObject.transform.position.y / 2, "Time", 3f, "easeType", iTween.EaseType.easeOutBounce));

        yield return wait;

        //iTween.MoveTo(text.gameObject, iTween.Hash("y", -text.gameObject.transform.position.y / 2, "Time", 2f, "easeType", iTween.EaseType.easeOutBounce));

        yield return wait;


    }

}
