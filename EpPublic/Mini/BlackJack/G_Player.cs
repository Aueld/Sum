using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_Player : MonoBehaviour
{
    [SerializeField]
    private G_Card cardScript;
    [SerializeField]
    private G_Deck deckScript;

    //[SerializeField]
    //private RectTransform StartPos;
    //private List<RectTransform> CardPos = new List<RectTransform>();

    // 손에 있는 카드 합
    public int handValue = 0;

    // 영혼의 파편 보유량
    private int soul = 1000;

    // 판에 깔린 카드
    public GameObject[] hand;
    
    // 다음 카드 숫자
    public int cardIndex = 0;

    //private void Start()
    //{
    //    //StartPos.position = new Vector2(0, 700);

    //    foreach (var card in hand)
    //    {
    //        CardPos.Add(card.GetComponent<RectTransform>());
    //        //card.transform.localPosition = new Vector3(0f, 700f, 0f);
    //    }
    //}

    // 두장 가지고 시작
    public void StartHand()
    {
        GetCardValue();
        GetCardValue();
    }

    // 카드 드로우
    public int GetCardValue()
    {
        //Vector2 curPos = StartPos.anchoredPosition;
        //Vector2 desPos = CardPos[cardIndex].anchoredPosition;

        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<G_Card>());
        
        hand[cardIndex].GetComponent<Image>().enabled = true;


        //iTween.ValueTo(hand[cardIndex], iTween.Hash("from", curPos, "to", desPos, "onupdate", "OnCardMove" , "time", 1.8f, "easetype", iTween.EaseType.easeOutSine));


        handValue += cardValue;

        cardIndex++;
        return handValue;
    }

    //public void OnCardMove(Vector2 tartgetPos)
    //{
    //    hand[cardIndex].GetComponent<RectTransform>().anchoredPosition = tartgetPos;
    //}

    public G_Card GetCard()
    {
        return cardScript;
    }

    // 승리시 보유 영혼 두배
    public void WIn()
    {
        soul *= 2;
    }

    // 패배시 보유 영혼 절반
    public void Lose()
    {
        soul /= 2;
    }

    // 소울 베팅 기능
    public void AdjustSoul(int amount)
    {
        soul += amount;
    }

    public int GetSoul()
    {
        return soul;
    }

    // 손 패 리셋
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<G_Card>().ResetCard();
            hand[i].GetComponent<Image>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
    }
}
