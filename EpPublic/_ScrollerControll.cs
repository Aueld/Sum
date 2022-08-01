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

    private TextMeshProUGUI tmp;

    public void AddNewObject(string type, string text)
    {
        
        var newObj = Instantiate(uiPrefabs, scrollRect.content).GetComponent<RectTransform>();
        
        tmp = newObj.GetComponent<TextMeshProUGUI>();
        

        uiObjects.Add(newObj);

        tmp.alignment = TextType(type);
        tmp.text = text;

        Canvas.ForceUpdateCanvases();

        float y = 0f;
        for(int i = 0; i < uiObjects.Count; i++)
        {
            Debug.Log(y);
            uiObjects[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjects[i].rect.height;
        }

        scrollRect.content.sizeDelta = new Vector2 (scrollRect.content.sizeDelta.x, y);
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
                return TextAlignmentOptions.MidlineLeft;
        }

        return TextAlignmentOptions.TopLeft;
    }

}
