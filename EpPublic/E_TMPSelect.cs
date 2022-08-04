using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_TMPSelect : MonoBehaviour
{
    public E_ScrollerControll e_ScrollerControll;

    public void ReturnOne()
    {
        if (e_ScrollerControll.TextEnd && !e_ScrollerControll.onRewordTyping)
        {
            e_ScrollerControll.StartRewordTyping(0);
            gameObject.SetActive(false);
        }
    }

    public void ReturnTwo()
    {
        if (e_ScrollerControll.TextEnd && !e_ScrollerControll.onRewordTyping)
        {
            e_ScrollerControll.StartRewordTyping(1);
            gameObject.SetActive(false);
        }
    }

    public void ReturnThree()
    {
        if (e_ScrollerControll.TextEnd && !e_ScrollerControll.onRewordTyping)
        {
            e_ScrollerControll.StartRewordTyping(2);
            gameObject.SetActive(false);
        }
    }
}
