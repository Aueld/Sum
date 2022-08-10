using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E_LoadCSVTable : MonoBehaviour
{
    private List<Dictionary<string, object>> eventTable;

    private void Awake()
    {
        // EventCSV
        eventTable = E_CSVReader.Read("EventScene");
    }

    // ID : 불러올 아이디, KEY : 불러올 키값
    // 씬 진입시 TextManager에 ID 값 전달
    protected List<string> GetEvent(string id, string key)
    {
        if (eventTable == null)
            eventTable = E_CSVReader.Read("EventScene");

        List<string> textList = new List<string>();

        for (int i = 0; i < eventTable.Count; i++)
        {
            if (eventTable[i]["ID"].ToString().Equals(id))
            {
                textList.Add(eventTable[i][key].ToString());
            }
        }
        return textList;
    }
}