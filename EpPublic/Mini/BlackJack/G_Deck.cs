using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;

public class G_Deck : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> cardSprites = new List<Sprite>();  // 카드 이미지

    [SerializeField]
    private int cardCount = 40;                             // 게임에 활용될 카드 최대 갯수

    private List<List<int>> cardValues = new List<List<int>>();         // 카드 값 저장 리스트
    private int currentIndex = 0;                           // 덱에서 현재 카드 인덱스

    private void Start()
    {
        GetCardValues();
    }

    // 카드 값 가져오기
    private void GetCardValues()
    {
        for (int i = 0; i < cardCount; i++)
        {
            // 테스트용 임의 랜덤값으로 덱에 카드 생성
            cardValues.Add( new List<int> {
                UnityEngine.Random.Range(-2, 10),
                UnityEngine.Random.Range(-2, 10),
                UnityEngine.Random.Range(-2, 10),
                UnityEngine.Random.Range(-2, 10)
            });

            cardValues[i].Sort();
            cardValues[i].Reverse();
        }
    }

    // 카드 섞기
    public void Shuffle()
    {
        for(int i = cardCount -1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(UnityEngine.Random.Range(0.0f, 1.0f) * cardCount - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            List<int> value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        currentIndex = 1;
    }

    // 딜러 카드
    public int DealCard(G_Card cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValueOfCard(cardValues[currentIndex][0]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    // 카드 뒷면 스프라이트
    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
