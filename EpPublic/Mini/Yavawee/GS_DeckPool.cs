using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DeckPool : MonoBehaviour
{
    private List<int> cardCord = new List<int>();

    // �ӽ� ī�� ���п� ���� �ο�
    private void Awake()
    {
        cardCord.Clear();

        // ī�� ���� �ο�
        for(int i = 0; i < 3; i++)
        {
            cardCord.Add(Random.Range(0, 20));
        }
    }

    // �ӽ� ī�� �ڵ� �ο�
    public List<int> GetCardCode()
    {
        return cardCord;
    }
}
