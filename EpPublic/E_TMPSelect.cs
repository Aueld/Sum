using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class E_TMPSelect : MonoBehaviour
{
    public E_TextManager textManager;
    public E_ScrollerControll scrollerControll;

    private GameObject select;
    private GameObject oldSelect;
    private int choice = - 1;

    private void Update()
    {
        // ������ �ι� ������ �۵�
        if (choice > -1)
        {
            if (select != null)
            {
                oldSelect = select.gameObject;
                select = null;
            }


            Debug.Log("����" + choice + select);
        }
    }

    // Button
    public void ReturnOne()
    {
        SelectClick(0);
    }

    // Button
    public void ReturnTwo()
    {
        SelectClick(1);
    }

    // Button
    public void ReturnThree()
    {
        SelectClick(2);
    }

    // ������ �ι� ������ �۵�
    private void SelectClick(int code)
    {
        if (scrollerControll.TextEnd && !scrollerControll.onRewordTyping)
        {
            if (oldSelect == EventSystem.current.currentSelectedGameObject)
            {
                scrollerControll.StartRewordTyping(code);

                gameObject.SetActive(false);
            }

            select = EventSystem.current.currentSelectedGameObject;
            
            choice = code;
        }
    }
}
