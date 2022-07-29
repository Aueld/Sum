using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _TextManager : _LoadCSVTable
{
    

    public TextMeshProUGUI[] Select = new TextMeshProUGUI[3];

    public _ScrollerControll scrollerControll;

    private List<string> _eventList = new List<string>();
    private List<string> _textList = new List<string>();
    private List<string> _selectList = new List<string>();
    private List<string> _selectCardList = new List<string>();
    private List<string> _rewordList = new List<string>();
    private List<string> _rewordTextList = new List<string>();

    private void Awake()
    {
        _eventList = GetEvent("10001", "Event");
        _textList = GetEvent("10001", "Text");
        _selectList = GetEvent("10001", "Select");
        _selectList = GetEvent("10001", "Select");
        _rewordList = GetEvent("10001", "Reword");
        _rewordTextList = GetEvent("10001", "RewordText");

        foreach (var text in _eventList)
            Debug.Log(text);

        foreach (var text in _textList)
            Debug.Log(text);

        foreach (var text in _selectList)
            Debug.Log(text);

    }

    private void OnEnable()
    {
        for(int i = 0; i < 5; i++)
            PrintMainText();
        PrintText(Select, _selectList);
    }

    private void PrintMainText()
    {
        scrollerControll.AddNewObject();

    }

    private void PrintText(TextMeshProUGUI[] tmp, List<string> list)
    {
        int index = 0;

        foreach (var text in list)
        {
            if (text.Length < 1)
                break;

            tmp[index++].text = text;
        }
    }
}
