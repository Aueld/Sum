using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_DeckPool : MonoBehaviour
{
    private List<int> cardCord = new List<int>();


    void Start()
    {
        cardCord.Clear();

        for(int i = 0; i < 3; i++)
        {
            cardCord.Add(Random.Range(0, 5));
        }
    }

    public List<int> GetCardCode()
    {
        return cardCord;
    }

}
