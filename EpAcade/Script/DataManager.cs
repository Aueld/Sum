using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region DataManager variable

    protected string filePath;                      // 파일 경로
    protected string data;                          // 저장 데이터

    protected List<int> sortScore = new List<int>();// 정렬 리스트

    private string[] splScore;
    private string scoreData;

    private string scoreMax;                        // 최고기록
    #endregion

    // Read Write Test Code
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
    //        data = GameManager.score.ToString();
    //        DataSave(filePath, data, 1);
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
    //        Debug.Log(DataRead(filePath));
    //    }
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        ScoreRank();
    //    }
    //}

    // 점수 불러오기
    protected void ScoreRank()
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
            if (count > 15) // 16등까지 데이터 저장
                break;
            
            scoreMax += i + ",";

            count++;
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "score.dat");
        DataSave(filePath, scoreMax, 0);
    }

    // 데이터 쓰기
    protected void DataSave(string filePath, string message, int saveType)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        FileStream fileStream = null;

        // 파일이 없다면 생성, 파일이 있다면 덮어쓰기
        if (saveType == 0)
            fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        else
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        writer.Write(message + ",");
        writer.Flush();
        writer.Close();
    }

    // 데이터 읽기
    protected string DataRead(string filePath)
    {

        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        // 파일이 있다면 불러오기, 파일이 없다면 텍스트 출력
        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();
        }

        else
            value = "기록된 점수가 없습니다.";

        return value;
    }
}