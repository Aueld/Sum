using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _ScrollerControll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject uiPrefabs;
    public List<RectTransform> uiObjects = new List<RectTransform>();

    private List<TextMeshProUGUI> contentTMP = new List<TextMeshProUGUI>();
    private List<List<char>> textChar = new List<List<char>>();
    private List<bool> TypingCheck = new List<bool>();

    private bool clickCheck = false;
    private Color color = new Color(1f, 1f, 1f, 0f);


    private void Update()
    {
        SkipTyping();
    }

    public void AddNewMainText(int index, string type, string text)
    {
        var newTMPRect = Instantiate(uiPrefabs, scrollRect.content).GetComponent<RectTransform>();

        contentTMP.Add(newTMPRect.GetComponent<TextMeshProUGUI>());
        uiObjects.Add(newTMPRect);
        
        TypingCheck.Add(false);

        if (!TypingCheck[0])
            TypingCheck[0] = true;

        contentTMP[index].alignment = TextType(type);

    
        contentTMP[index].text = text;

        if (contentTMP[index].alignment == TextAlignmentOptions.Center)
        {
            contentTMP[index - 1].text += "\n\n";
            contentTMP[index].text += "\n\n\n";
        }


        contentTMP[index].color = color;

        textChar.Add(new List<char>());
        for (int i = 0; i < text.Length; i++)
        {
            textChar[index].Add(text[i]);
            if (text[i] == '.')
                textChar[index].Add('\n');

        }

        Canvas.ForceUpdateCanvases();

        float y = 0f;

        for(int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjects[i].rect.height;
        }

        scrollRect.content.sizeDelta = new Vector2 (scrollRect.content.sizeDelta.x, y);
    }

    public void StartTyping(int CIndex, int maxIndex)
    {
        StartCoroutine(Typing(CIndex, maxIndex));
    }

    private IEnumerator Typing(int tempIndex, int maxIndex)
    {
        yield return new WaitForSeconds(0.5f);

        if (TypingCheck[tempIndex])
        {
            contentTMP[tempIndex].text = "";
            contentTMP[tempIndex].color = Color.white;
            Canvas.ForceUpdateCanvases();

            foreach (var cha in textChar[tempIndex])
            {
                if (clickCheck)
                    yield break;

                if (cha == '\n')
                    yield return new WaitForSeconds(0.05f);

                contentTMP[tempIndex].text += cha;
                yield return new WaitForSeconds(0.02f);
            }

            TypingCheck[tempIndex] = false;
            try
            {
                TypingCheck[tempIndex + 1] = true;
            } catch { }

            if(tempIndex < maxIndex)
                StartCoroutine(Typing(tempIndex + 1, maxIndex));
        }
    }

    private void SkipTyping()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCheck = true;

            string temp;

            for (int i = 0; i < contentTMP.Count; i++)
            {
                temp = "";

                foreach (var cha in textChar[i])
                    temp += cha;

                contentTMP[i].text = temp;
                contentTMP[i].color = Color.white;
            }
        }
    }

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
