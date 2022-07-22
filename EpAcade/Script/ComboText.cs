using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboText : MonoBehaviour
{
    #region Combo variable

    public int size = 100;

    private Text text;

    #endregion

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (GameManager.comboSet)
            StartCoroutine(ComboEff());


        text.text = GameManager.combo + " Combo";
    }

    // 콤보시 텍스트 효과
    private IEnumerator ComboEff()
    {
        text.fontSize = (int) (size * 1.5f);

        if (GameManager.textShadowCount >= 2)
        {
            GameManager.textShadowCount = 0;
            GameManager.comboSet = false;
        }
        else
            GameManager.textShadowCount++;

        while (true)
        {
            if (text.fontSize < size)
            {
                text.fontSize = size;
                yield break;
            }

            text.fontSize -= 2;

            yield return GameManager.wait;
        }
    }
}
