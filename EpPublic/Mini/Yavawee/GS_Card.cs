using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GS_Card : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int code;

    private void OnEnable()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetCardCode(int getCode)
    {
        code = getCode;

        text.text = code.ToString();
    }

}
