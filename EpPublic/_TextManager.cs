using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _TextManager : _LoadCSVTable
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI[] Select = new TextMeshProUGUI[3];

    public _ScrollerControll scrollerControll;

    public string ID;

    private List<string> _eventList = new List<string>();
    private List<string> _textTypeList = new List<string>();
    private List<string> _textList = new List<string>();
    private List<string> _selectList = new List<string>();
    private List<string> _selectCardList = new List<string>();
    private List<string> _rewordList = new List<string>();
    private List<string> _rewordTextList = new List<string>();


    // 씬 시작시 ID 코드 입력 받아야 해당하는 이벤트가 나옴
    private void Awake()
    {
        _eventList = GetEvent(ID, "Event");
        _textTypeList = GetEvent(ID, "TextType");
        _textList = GetEvent(ID, "Text");
        _selectList = GetEvent(ID, "Select");
        _selectList = GetEvent(ID, "Select");
        _rewordList = GetEvent(ID, "Reword");
        _rewordTextList = GetEvent(ID, "RewordText");
    }

    private void OnEnable()
    {
        PrintTitle(_eventList);

        PrintMainText(_textTypeList, _textList);


        PrintText(Select, _selectList);

    }

    private void PrintTitle(List<string> list)
    {
        foreach (var text in list)
        {
            if (text.Length > 0)
                title.text = text;
        }
    }

    private void PrintMainText(List<string> type, List<string> list)
    {
        int index = 0;

        foreach (var text in list)
        {
            scrollerControll.AddNewMainText(index, type[index], text);

            index++;
        }

        scrollerControll.StartTyping(0, index);
    }

    private void PrintText(TextMeshProUGUI[] tmp, List<string> list)
    {
        int index = 0;

        foreach (var text in list)
        {
            if (text.Length > 0)
                tmp[index++].text = text;
        }
    }
}
