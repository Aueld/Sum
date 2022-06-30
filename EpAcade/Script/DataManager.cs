using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string filePath;
    private string message;
    
    private string scoreData;
    private string[] splScore;
    private List<int> sortScore = new List<int>();

    private string scoreMax;

    //Test Update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
            message = GameManager.score.ToString();

            DataSave(filePath, message, 1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
            Debug.Log(DataRead(filePath));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ScoreRank();
        }
    }

    private void ScoreRank()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
        scoreData = DataRead(filePath);
        splScore = scoreData.Split(',');

        foreach (string score in splScore)
        {
            try {
                sortScore.Add(int.Parse(score));
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }

        sortScore.Sort();
        sortScore.Reverse();

        int count = 0;
        scoreMax = "";

        foreach(int i in sortScore)
        {
            if (count > 9)
                break;
            
            scoreMax += i + ",";

            Debug.Log(i + " ");

            count++;
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
        DataSave(filePath, scoreMax, 0);
    }

    void DataSave(string filePath, string message, int saveType)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        FileStream fileStream = null;

        if (saveType == 0)
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        else
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        writer.Write(message + ",");
        writer.Flush();
        writer.Close();
    }

    string DataRead(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();
        }

        else
            value = "파일이 없습니다.";

        return value;
    }
}