using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private Image FadeImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image endImage;

    private FadeInOut fadeInOut;
    private float scrollSpeed = 0.025f;

    private void Awake()
    {
        fadeInOut = GetComponent<FadeInOut>();
    }

    private void FixedUpdate()
    {
        if (!fadeInOut.endCheck)
            return;
        else
        {
            StartCoroutine(fadeInOut.CoFadeIn(1f, FadeImage));

            text.transform.Translate(Vector3.up * scrollSpeed);

            if (endImage.transform.position.y > 0)
                StartCoroutine(WaitEnd(3f));
        }
    }

    private IEnumerator WaitEnd(float fadeTime)
    {
        fadeInOut.endCheck = false;
        StartCoroutine(fadeInOut.CycleInOut(fadeTime, endImage));

        // ���� �� ����ȭ������ ���ϴ�

        yield return null;
    }
}
