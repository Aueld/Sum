using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//public static class Utils 
//{
//    public static IEnumerator EventResult(int code)
//    {
//        yield return null;
//        switch (code)
//        {
//            case 0:
//                break;
//        }
//    }
//}

public class E_TextManager : E_LoadCSVTable
{
    // 씬 시작시 ID 코드 입력 받아야 해당하는 이벤트가 나옴
    public string ID;

    #region Public Variable

    private static E_TextManager instance;

    public TextMeshProUGUI title;
    public TextMeshProUGUI[] Select;
    public E_ScrollerControll scrollerControll;

    public TextMeshProUGUI reword;
    public Image Exit;

    public Image image;
    
    Sprite[] asdasdas;

    [SerializeField]
    private E_ImageScriptableObject imagePool;

    #endregion

    #region List
    [SerializeField]
    private List<Sprite> sprite;

    private List<string> _eventList = new List<string>();
    private List<string> _textTypeList = new List<string>();
    private List<string> _textList = new List<string>();
    private List<string> _selectList = new List<string>();
    private List<string> _rewordTextList = new List<string>();
    private List<string> _rewordList = new List<string>();
    #endregion

    private bool PrintSelect = false;
    
    #region Unity Function Awake, Enable, LateUpdate

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
        Application.targetFrameRate = 60;

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

        reword.gameObject.SetActive(false);
        Exit.gameObject.SetActive(false);

        PrintTitle(_eventList);
        PrintMainText(_textTypeList, _textList, _rewordTextList);
    }

    private void Start()
    {
        //foreach (var img in imagePool.ImageData)
        //{
        //    sprite = img.sprite;
        //}


        

    }

    public void Method(int code)
    {
        switch (code)
        {
            case 0:
                break;


        }
    }


    private void LateUpdate()
    {
        if (scrollerControll.TextEnd && !PrintSelect)
            PrintSelectText(Select, _selectList);

    }
    #endregion

    #region Print
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
            scrollerControll.AddNewMainText(index, type[index], text);

            index++;
        }

        scrollerControll.AddNewResultText(reList);

        scrollerControll.StartTyping(0, index);
    }

    private void PrintSelectText(TextMeshProUGUI[] tmp, List<string> list)
    {
        PrintSelect = true;

        int index = 0;

        foreach (var text in list)
        {
            if (text.Length > 0)
                tmp[index++].text = text;
        }
    }

    public void PrintReword(int num)
    {
        Exit.gameObject.SetActive(true);
        reword.gameObject.SetActive(true);

        reword.text = _rewordList[num];
    }
    #endregion
}
