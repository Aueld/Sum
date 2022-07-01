using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewItem : DataManager
{
    public GameObject parent;
    public Text rankText;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        int i = 1;
        int y = 0;

        ScoreRank();

        foreach (int data in sortScore)
        {
            if (i > 8)
                break;

            switch (i)
            {
                case 1:
                    rankText.text = i + " st\t\t" + data;
                    break;
                case 2:
                    rankText.text = i + " ed\t\t" + data;
                    break;
                case 3:
                    rankText.text = i + " rd\t\t" + data;
                    break;
                default:
                    rankText.text = i + " th\t\t" + data;
                    break;
            }

            var Index = Instantiate(rankText, new Vector3(0, y, 0), Quaternion.identity);
            Index.transform.SetParent(parent.transform);

            y -= 200;
            i++;
        }   
    }
}
