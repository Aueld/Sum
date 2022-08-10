using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GS_GameManager : MonoBehaviour
{
    public GS_DeckPool deck;

    [SerializeField] private List<GS_Card> card;


    private void Start()
    {
        InitGame();
    }


    private void Update()
    {

    }

    private void InitGame()
    {
        for (int i = 0; i < 3; i++)
        {
            card[i].SetCardCode(deck.GetCardCode()[i]);
        }
    }

    private void GameStart()
    {

    }

    public void Shuffle()
    {
        List<GS_Card> suffleDeck = ShuffleList(card);

        for (int i = 0; i < 3; i++)
            suffleDeck[i].SetCardCode(deck.GetCardCode()[i]);


    }

    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }
}
