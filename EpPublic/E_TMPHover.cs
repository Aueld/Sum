using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class E_TMPHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI tmp;
    private Toggle toggle;

    private Color color;
    private Color faceColor;
    private Color outColor;

    private void Start()
    {
        toggle = GetComponentInParent<Toggle>();

        tmp = GetComponent<TextMeshProUGUI>();

        color = new Color(0.55f, 0.55f, 0.55f);

        faceColor = tmp.faceColor;
        outColor = tmp.outlineColor;
    }

    private void LateUpdate()
    {
        if (!toggle.isOn)
        {
            tmp.color = color;
            tmp.faceColor = color;
            tmp.outlineColor = color;
        }
        else
        {
            tmp.color = Color.white;
            tmp.faceColor = faceColor;
            tmp.outlineColor = outColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
        ((IPointerEnterHandler)toggle).OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.isOn = false;
        ((IPointerExitHandler)toggle).OnPointerExit(eventData);
    }
}
