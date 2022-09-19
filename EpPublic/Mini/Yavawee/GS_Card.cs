using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GS_Card : MonoBehaviour, IPointerClickHandler
{
    public GameObject image;

    private TextMeshProUGUI text;
    private int code;

    private void Awake()
    {
        // 숫자 가림
        image.SetActive(true);

        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetCardCode(int getCode)
    {
        code = getCode;

        text.text = code.ToString();
    }

    public void HideCount()
    {
        image.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 임시 가리기입니다.
        image.SetActive(false);
    }
}
