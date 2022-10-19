using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GS_Card : MonoBehaviour
{
    public GameObject CardBack;

    private TextMeshProUGUI text;
    private int code;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetCardCode(int getCode)
    {
        code = getCode;

        text.text = code.ToString();
    }

    public GameObject GetCardBack()
    {
        return CardBack;
    }
}
