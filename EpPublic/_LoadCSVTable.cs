using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class _LoadCSVTable : MonoBehaviour
{
    private List<Dictionary<string, object>> eventTable;

    private void Awake()
    {
        eventTable = _CSVReader.Read("EventScene");
    }

    // ID : 불러올 아이디, KEY : 불러올 키값
    protected List<string> GetEvent(string id, string key)
    {
        if (eventTable == null)
            eventTable = _CSVReader.Read("EventScene");

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

    //protected List<string> GetEventText(string id)
    //{
    //    if (eventTable == null)
    //        eventTable = _CSVReader.Read("EventScene");

    //    List<string> textList = new List<string>();

    //    for (int i = 0; i < eventTable.Count; i++)
    //    {
            
    //        if (eventTable[i]["ID"].ToString().Equals(id))
    //        {
    //            textList.Add(eventTable[i]["Text"].ToString());
    //        }
    //    }
    //    return textList;
    //}

    //protected List<string> GetSeletText(int id)
    //{
    //    if (eventTable == null)
    //        eventTable = _CSVReader.Read("EventScene");

    //    List<string> textList = new List<string>();

    //    for (int i = 0; i < eventTable.Count; i++)
    //    {

    //        if (eventTable[i]["ID"].ToString().Equals(id))
    //        {
    //            textList.Add(eventTable[i]["SelectText"].ToString());
    //        }
    //    }
    //    return textList;
    //}
}