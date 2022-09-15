using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class G_Card : MonoBehaviour
{
    [SerializeField]
    private Sprite cardBackSprite;  // 카드 뒷면 이미지

    private int cardValue = 0;       // 카드 값

    // 카드 값 getter
    public int GetValueOfCard()
    {
        return cardValue;
    }

    // 카드값 setter
    public void SetValueOfCard(int newValue)
    {
        cardValue = newValue;
        GetComponentInChildren<TextMeshProUGUI>().text = cardValue.ToString();
    }

    // 카드 이미지 setter
    public void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<Image>().sprite = newSprite;
    }

    // 카드 리셋
    public void ResetCard()
    {
        gameObject.GetComponent<Image>().sprite = cardBackSprite;
        cardValue = 0;
    }
}
