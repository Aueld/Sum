using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ScenesManager : DataManager
{
    private GameObject canvas;
    private bool check;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "TitleMenu":
                check = false;
                canvas = GameObject.Find("OPTION").gameObject;
                canvas.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void KeyMode()
    {
        Toggle toggle = GetComponentInChildren<Toggle>();

        GameManager.btnNum = toggle.isOn;
    }

    public void ViewOption()
    {
        switch (check)
        {
            case true:
                canvas.SetActive(false);
                check = false;
                break;

            case false:
                canvas.SetActive(true);

                Toggle toggle = GetComponentInChildren<Toggle>();

                toggle.isOn = GameManager.btnNum;

                check = true;
                break;
        }
    }

    public  void GameRetry()
    {
        SaveScore();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        SaveScore();

        SceneManager.LoadScene("TitleMenu");
    }

    public void Exit()
    { 
        Application.Quit();
    }

    public void ViewScoreRank()
    {
        SceneManager.LoadScene("ScoreMenu");
    }

    private void SaveScore()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
        data = GameManager.score.ToString();

        DataSave(filePath, data, 1);

        SceneManager.LoadScene("TitleMenu");
    }
}
