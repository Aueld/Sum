using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DeckPool : MonoBehaviour
{
    private List<int> cardCord = new List<int>();

    // 임시 카드 구분용 숫자 부여
    private void Awake()
    {
        cardCord.Clear();

        // 카드 세장 부여
        for(int i = 0; i < 3; i++)
        {
            cardCord.Add(Random.Range(0, 20));
        }
    }

    // 임시 카드 코드 부여
    public List<int> GetCardCode()
    {
        return cardCord;
    }
}
