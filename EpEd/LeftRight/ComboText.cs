using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboText : MonoBehaviour
{
    #region Combo variable

    public int size = 100;      // 텍스트 사이즈

    private TextMeshProUGUI text;

    #endregion

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        if (TapGameManager.comboSet)
            StartCoroutine(ComboEff());

        text.text = TapGameManager.combo + " Combo";
    }

    // 콤보시 텍스트 효과, 텍스트 두개에 효과주어 입체감
    private IEnumerator ComboEff()
    {
        text.fontSize = (int) (size * 1.5f);

        if (TapGameManager.textShadowCount >= 2)
        {
            TapGameManager.textShadowCount = 0;
            TapGameManager.comboSet = false;
        }
        else
            TapGameManager.textShadowCount++;

        while (true)
        {
            if (text.fontSize < size)
            {
                text.fontSize = size;
                yield break;
            }

            text.fontSize -= 2;

            yield return TapGameManager.wait;
        }
    }
}
