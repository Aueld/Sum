using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private static List<GameObject> ViewOBJ = new List<GameObject>();
    
    static int layer = 0;

    private void Awake()
    {
        ViewOBJ.Clear();

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            ViewOBJ.Add(gameObject.transform.GetChild(i).gameObject);
        }

        ViewOBJ.Reverse();

        SetLayer();
    }


    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (layer > 0)
                {
                    layer--;
                    ViewOn();
                }
            } 
        }
    }


    private void ViewOn()
    {
        if (!ViewOBJ[layer].activeSelf)
            ViewOBJ[layer].SetActive(true);
    }


    public static void StartView()
    {
        ViewOBJ[layer].SetActive(false);

        if(!ViewOBJ[layer + 1].activeSelf)
            ViewOBJ[layer + 1].SetActive(true);

        layer++;    
    }

    private void SetLayer()
    {
        for(int i = 0; i < ViewOBJ.Count; i++)
            ViewOBJ[i].SetActive(false);

        ViewOBJ[layer].SetActive(true);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
