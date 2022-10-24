using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadSceneManager : MonoBehaviour
{
    private static readonly List<GameObject> ViewOBJ = new List<GameObject>();
    private static int layer = 0;

    private void Awake()
    {
        ViewOBJ.Clear();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            ViewOBJ.Add(gameObject.transform.GetChild(i).gameObject);
        }

        ViewOBJ.Reverse();

        SetLayer();
    }

    private void Start()
    {
        if (GameManager.instance.gameQueue.Count > 0)
        {
            for (int i = 0; i < ViewOBJ.Count; i++)
            {
                ViewOBJ[i].SetActive(false);
            }

            GameStart();
        }
        else
        {
            for (int i = 0; i < ViewOBJ.Count; i++)
            {
                ViewOBJ[i].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewBackOn();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ViewBackOn();
            }
        }
    }

    private void SetLayer()
    {
        for (int i = 0; i < ViewOBJ.Count; i++)
            ViewOBJ[i].SetActive(false);

        ViewOBJ[layer].SetActive(true);
    }

    public void GameStart()
    {
        GameManager.instance.GameStarter();
    }

    public static void ViewFrontOn()
    {
        if (layer < ViewOBJ.Count)
        {
            ViewOBJ[layer].SetActive(false);

            layer++;

            ViewOBJ[layer].SetActive(true);
        }
    }

    private void ViewBackOn()
    {
        if (layer > 0)
        {
            layer--;

            ViewOBJ[layer].SetActive(true);
        }
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
