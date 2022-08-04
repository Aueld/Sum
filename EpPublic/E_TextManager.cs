using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class E_TextManager : E_LoadCSVTable
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI[] Select;
    public E_ScrollerControll e_ScrollerControll;

    public TextMeshProUGUI reword;
    public TextMeshProUGUI Exit;

    public Image image;
    public Sprite[] sprite;

    public string ID;

    private List<string> _eventList = new List<string>();
    private List<string> _textTypeList = new List<string>();
    private List<string> _textList = new List<string>();
    private List<string> _selectList = new List<string>();

    public List<string> _rewordTextList = new List<string>();
    public List<string> _rewordList = new List<string>();

    private bool PrintSelect = false;

    // 씬 시작시 ID 코드 입력 받아야 해당하는 이벤트가 나옴
    private void Awake()
    {
        _eventList = GetEvent(ID, "Event");
        _textTypeList = GetEvent(ID, "TextType");
        _textList = GetEvent(ID, "Text");
        _selectList = GetEvent(ID, "Select");

        _rewordTextList = GetEvent(ID, "RewordText");
        _rewordList = GetEvent(ID, "Reword");
    }

    private void OnEnable()
    {
        image.sprite = sprite[0];

        PrintTitle(_eventList);
        PrintMainText(_textTypeList, _textList, _rewordTextList);
    }

    private void LateUpdate()
    {
        if (e_ScrollerControll.TextEnd && !PrintSelect)
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

    private void PrintMainText(List<string> type, List<string> list, List<string> reList)
    {
        int index = 0;

        foreach (var text in list)
        {
            e_ScrollerControll.AddNewMainText(index, type[index], text);

            index++;
        }

        index = 0;

        foreach (var text in reList)
        {
            if (text.Length > 0)
                e_ScrollerControll.AddNewResultText(index, text);

            index++;
        }

        e_ScrollerControll.StartTyping(0, index);
    }


    private void PrintText(TextMeshProUGUI[] tmp, List<string> list)
    {
        PrintSelect = true;

        int index = 0;

        foreach (var text in list)
        {
            if (text.Length > 0)
                tmp[index++].text = text;
        }
    }
}
