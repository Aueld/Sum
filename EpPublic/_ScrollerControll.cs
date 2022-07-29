using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _ScrollerControll : MonoBehaviour
{
    public ScrollRect scrollRect;

    public float space = 30f;
    public GameObject uiPrefabs;
    public List<RectTransform> uiObjects = new List<RectTransform>();

    public void AddNewObject()
    {
        var newObj = Instantiate(uiPrefabs, scrollRect.content).GetComponent<RectTransform>();
        uiObjects.Add(newObj);

        float y = 0f;
        for(int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjects[i].sizeDelta.y + space;
        }

        scrollRect.content.sizeDelta = new Vector2 (scrollRect.content.sizeDelta.x, y);
    }
}
