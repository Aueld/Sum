using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class E_ScrollerControll : MonoBehaviour
{
    // WaitForSeconds
    private static readonly WaitForSeconds typingWaitEnter = new WaitForSeconds(0.2f);
    private static readonly WaitForSeconds typingWait = new WaitForSeconds(0.015f);

    #region PUBLIC VAL
    public E_TextManager TextManager;

    // 스크롤 바
    public ScrollRect scrollRect;

    public GameObject UIPrefabs;

    // View Reword
    public GameObject RewordBar;
    public GameObject RewordText;
    
    public bool TextEnd;
    public bool onRewordTyping;
    #endregion

    #region PRIVATE VAL
    // List
    private List<RectTransform> uiObjectsRectTransform = new List<RectTransform>();
    private List<TextMeshProUGUI> contentTMP = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> contentReTMP = new List<TextMeshProUGUI>();
    private List<List<char>> textChar = new List<List<char>>();
    private List<List<char>> textRewordChar = new List<List<char>>();
    private List<bool> TypingCheck = new List<bool>();

    private Color color = new Color(1f, 1f, 1f, 0f);        // 알파값 0

    private bool createBar;                                 // 결과 바 생성 유무
    private static Image bar;                               // Reword Bar Image
    private static int reNum;                               // 결과 인덱스
    private static float reY;                               // 결과 콘텐츠 Y 값

    private bool clickCheck;                                // 클릭 유무

    #endregion

    #region Unity Function (Awake, Update)
    // 초기화
    private void Awake()
    {
        reY = 0;
        reNum = 0;

        TextEnd = false;
        clickCheck = false;
        createBar = false;
        onRewordTyping = false;
    }

    // 업데이트
    private void Update()
    {
        SkipTyping();
    }
    #endregion

    // 스크롤뷰에 메인 TMP 추가
    public void AddNewMainText(int index, string type, string text)
    {
        var newTMPRect = Instantiate(UIPrefabs, scrollRect.content).GetComponent<RectTransform>();

        contentTMP.Add(newTMPRect.GetComponent<TextMeshProUGUI>());
        uiObjectsRectTransform.Add(newTMPRect);
        
        TypingCheck.Add(false);

        if (!TypingCheck[0])
            TypingCheck[0] = true;

        contentTMP[index].alignment = TextType(type);
        contentTMP[index].text = text;
        contentTMP[index].color = color;

        if (contentTMP[index].text.Length < 1)
            contentTMP[index].text += "\n";

        textChar.Add(new List<char>());
        for (int i = 0; i < contentTMP[index].text.Length; i++)
        {
            textChar[index].Add(contentTMP[index].text[i]);
        }

        AddContent();
    }

    // 스크롤뷰에 구분 바, 결과 TMP 추가
    public void AddNewResultText(int index, string text)
    {
        if (!createBar)
        {
            createBar = true;
            var newReBarRect = Instantiate(RewordBar, scrollRect.content).GetComponent<RectTransform>();
            
            bar = newReBarRect.GetComponentInChildren<Image>();
            
            uiObjectsRectTransform.Add(newReBarRect);

            AddContent();
        }

        var newTMPRect = Instantiate(RewordText, scrollRect.content).GetComponent<RectTransform>();

        contentReTMP.Add(newTMPRect.GetComponent<TextMeshProUGUI>());
        uiObjectsRectTransform.Add(newTMPRect);

        contentReTMP[index].color = color;

        textRewordChar.Add(new List<char>());
        for (int i = 0; i < text.Length; i++)
        {
            textRewordChar[index].Add(text[i]);
        }

        AddContentResult();
    }

    // 콘텐츠 위치 지정
    public void AddContent()
    {
        float y = 0f;

        for (int i = 0; i < uiObjectsRectTransform.Count; i++)
        {
            uiObjectsRectTransform[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjectsRectTransform[i].rect.height;
        }

        reNum++;
        reY = y;

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }

    // 결과 위치 지정
    public void AddContentResult()
    {
        for(int i = reNum; i < uiObjectsRectTransform.Count; i++ )
            uiObjectsRectTransform[i].anchoredPosition = new Vector2(0f, -reY);
        
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, reY);
    }

    #region 타이핑

    // 타이핑 시작 함수
    public void StartTyping(int CIndex, int maxIndex)
    {
        StartCoroutine(Typing(CIndex, maxIndex));
    }

    // 타이핑 코루틴
    private IEnumerator Typing(int index, int maxIndex)
    {
        yield return typingWaitEnter;

        if (index >= maxIndex)
            yield break;

        if (TypingCheck[index])
        {
            contentTMP[index].text = "";
            contentTMP[index].color = Color.white;

            foreach (var cha in textChar[index])
            {
                if (clickCheck)
                    yield break;

                if (cha == '\n')
                    yield return typingWaitEnter;

                contentTMP[index].text += cha;

                yield return typingWait;
            }

            TypingCheck[index] = false;

            if (TypingCheck.Count > index + 1)
            {
                TypingCheck[index + 1] = true;
                StartCoroutine(Typing(index + 1, maxIndex));
            }
            else
            {
                TextEnd = true;
            }
        }
    }

    // 결과 타이핑 시작 함수
    public void StartRewordTyping(int index)
    {
        StartCoroutine(RewordTyping(index));
        // content height 값 변경
        // https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=kki5150&logNo=221151602571
    }

    // 결과 타이핑 코루틴
    private IEnumerator RewordTyping(int index)
    {
        clickCheck = false;

        bar.color = Color.white;

        onRewordTyping = true;
        yield return typingWaitEnter;

        contentReTMP[index].text = "";
        contentReTMP[index].color = Color.white;

        foreach (var cha in textRewordChar[index])
        {
            if (clickCheck)
                yield break;

            contentReTMP[index].text += cha;
            yield return typingWait;
        }
        onRewordTyping = false;

        yield return typingWaitEnter;


        Debug.Log("결과 나옴" + index);

    }

    // 타이핑 스킵
    private void SkipTyping()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCheck = true;

            StartCoroutine(SKIP());
        }
    }

    private IEnumerator SKIP()
    {
        if (TextEnd)
            yield break;

        for (int i = 0; i < contentTMP.Count; i++)
        {
            contentTMP[i].text = "";
            
            foreach (var cha in textChar[i])
                contentTMP[i].text += cha;

            contentTMP[i].color = Color.white;

            yield return null;
        }

        TextEnd = true;
    }
    #endregion
    
    // 텍스트 AlignmentOptions
    private TextAlignmentOptions TextType(string type)
    {
        switch (type)
        {
            case "top":
                return TextAlignmentOptions.TopLeft;
            case "bottom":
                return TextAlignmentOptions.BottomLeft;
            case "middle":
                return TextAlignmentOptions.Center;
        }

        return TextAlignmentOptions.TopLeft;
    }
}
