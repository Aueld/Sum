using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameObj;
    [SerializeField] private Material background;
    [SerializeField] private Material state;
    [SerializeField] private GameObject GUIGameStop;
    
    public TextMeshPro[] GUIStateText;

    public static GameManager instance;

    public int selectLevel;
    public int reLayer = 0;
    public int[] State;
    public string loadScene;
    public bool gameSet;
    public bool[] clearCheck;

    public Queue<string> gameQueue = new Queue<string>();

    // 게임 큐
    private List<string> gameNum = new List<string>()
    {
        "Gyro",
        "DropBall",
        "LeftRight",
        "spinBall",
        "Memory"

    };
    
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

    public void GameStarter()
    {
        if (gameSet)
        {
            System.Random rand = new System.Random();
            var shuffled = gameNum.OrderBy(_ => rand.Next()).ToList();
            gameQueue = new Queue<string>(shuffled);

            gameSet = false;
        }

        if (gameQueue.Count != 0)
        {
            NextScene(gameQueue);
            
            GUIGameStop.SetActive(true);
            Time.timeScale = 0;
        }
        else
            StateData.ViewState(gameObj, background, state);
    }

    public void NextScene(Queue<string> queue)
    {
        selectLevel = 1;

        if (queue.Count > 0)
            SceneManager.LoadScene(queue.Dequeue());
    }

    // 게임 시작 전 일시정지
    public void GameStop()
    {
        GUIGameStop.SetActive(false);
        Time.timeScale = 1;
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
