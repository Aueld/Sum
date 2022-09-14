using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class G_Player : MonoBehaviour
{
    [SerializeField]
    private G_Card cardScript;
    [SerializeField]
    private G_Deck deckScript;

    // 손에 있는 카드 합
    public int handValue = 0;

    // 영혼의 파편 보유량
    private int soul = 1000;

    // 판에 깔린 카드
    public GameObject[] hand;
    
    // 다음 카드 숫자
    public int cardIndex = 0;

    private List<Vector3> CardPos = new List<Vector3>();

    private void Awake()
    {
        foreach (var card in hand)
        {
            CardPos.Add(card.transform.localPosition);

            card.transform.localPosition = new Vector3(0f, 800f, 0f);
            card.transform.localRotation = new Quaternion(0f, 0f, 180f, 0);
        }
    }

    // 두장 가지고 시작
    public void StartHand()
    {
        GetCardValue();
        GetCardValue();
    }

    // 카드 드로우
    public int GetCardValue()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<G_Card>());
        
        hand[cardIndex].GetComponent<Image>().enabled = true;

        OnCardMove(cardIndex);

        handValue += cardValue;

        cardIndex++;
        return handValue;
    }


    public void OnCardMove(int index)
    {
        hand[index].GetComponent<RectTransform>().DOAnchorPos(CardPos[index], 1f, false).SetEase(Ease.OutExpo);
        hand[index].GetComponent<RectTransform>().DORotate(Vector3.zero, 1f).SetEase(Ease.OutExpo);
    }

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
