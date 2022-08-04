using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_ScrollingEffact : MonoBehaviour
{
    private static readonly WaitForSeconds ViewTime = new WaitForSeconds(0.8f);
    private static readonly WaitForSeconds wait = new WaitForSeconds(0.04f);

    public RectTransform content;
    public Image Scrollbar;
    public Image Handle;

    private ScrollRect scrollRect;
    private Color color;
    private bool mouseDown = false;
    

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();

        color = new Color(1, 1, 1, 0);

        Scrollbar.color = color;
        Handle.color = color;
    }

    private void Update()
    {
        // ���빰�� ��ũ�� �� ���� �ƴ϶��, 660 �Ŀ� ���� �ʿ�
        if (content.rect.height < 660)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            var pos = new Vector2(10f, Mathf.Sin(Time.time * 10f) * 0.1f);
            scrollRect.content.localPosition = pos;

            mouseDown = true;

            StopCoroutine(Click());

            StartCoroutine(Click());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;

            StartCoroutine(Click());
        }
    }

    // ���콺 �Է½� ��ũ�ѹ� ��� ���� �� ����ȭ
    private IEnumerator Click()
    {
        color = new Color(1, 1, 1, 1);
        Scrollbar.color = color;
        Handle.color = color;

        if (mouseDown)
            yield break;

        yield return ViewTime;

        while (true)
        {
            if(mouseDown)
            {
                color = new Color(1, 1, 1, 1);
                Scrollbar.color = color;
                Handle.color = color;

                yield break;
            }


            if(color.a < 0)
            {
                color.a = 0;
                yield break;
            }

            color.a -= 0.08f;

            Scrollbar.color = color;
            Handle.color = color;

            yield return wait;
        }
    }
}
