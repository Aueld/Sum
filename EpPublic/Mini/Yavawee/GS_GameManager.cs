using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_GameManager : MonoBehaviour
{
    public GS_DeckPool deck;

    [SerializeField] private List<GameObject> cards = new List<GameObject>();

    List<GS_Card> cardScripts = new List<GS_Card>();
    List<Vector2> CardPos = new List<Vector2>();

    private bool gameStartWait = true;

    private void Start()
    {
        InitGame();
        GameStart();
    }

    private void InitGame()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cardScripts.Add(cards[i].GetComponent<GS_Card>());
            CardPos.Add(cards[i].transform.localPosition);

            cardScripts[i].SetCardCode(deck.GetCardCode()[i]);

            cards[i].transform.localPosition = new Vector2(0f, 800f);
            cards[i].transform.localRotation = new Quaternion(0, 0, 180f, 0);
        }
    }

    private void GameStart()
    {
        int index = 0;
        foreach(var card in cards)
        {
            OnCardMove(index++);
        }
    }

    public void OnCardMove(int index)
    {
        if ((Vector2)cards[index].transform.position == CardPos[index])
            return;

        // 한번 위로 올라갔다가 내려가는 방식
        // Y를 400으로 올린 후 OnComplete를 통해 종료 후 다음 DOTween을 실행
        cards[index].GetComponent<RectTransform>().DOAnchorPosY(100f, 0.2f, false).SetEase(Ease.InExpo).OnComplete(() =>
        {
            cards[index].GetComponent<RectTransform>().DOAnchorPos(CardPos[index], 0.2f, false).SetEase(Ease.OutExpo);
        });

        // 좌우로만 섞이는 방식
        //cards[index].GetComponent<RectTransform>().DOAnchorPos(CardPos[index], 0.2f, false).SetEase(Ease.OutExpo);

        if (!MiniGameManager.instance.isShuffle)
            cards[index].GetComponent<RectTransform>().DORotate(Vector3.zero, 1f).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                // 게임 시작 전 시작 버튼이 클릭되어 카드 뒤집기가 풀리는 현상 방지입니다
                if (gameStartWait)
                    gameStartWait = false;
            });
    }

    public void Shuffle()
    {
        if (MiniGameManager.instance.isShuffle || gameStartWait)
            return;

        MiniGameManager.instance.isShuffle = true;

        for (int index = 0; index < 3; index++)
        {
            // 임시로 가리는 함수입니다.
            cards[index].GetComponent<GS_Card>().HideCount();

            // 카드 뒷면이 필요합니다
            // 카드를 뒤집어 섞는 방식
            cards[index].GetComponent<RectTransform>().DORotate(Vector3.up * 180f, 0.2f).SetEase(Ease.OutExpo);
        }
        StartCoroutine(NShuffle());
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

    private IEnumerator NShuffle()
    {
        for (int i = 0; i < Random.Range(10, 15); i++)
        {
            ShuffleList(CardPos);

            for (int j = 0; j < cards.Count; j++)
            {
                OnCardMove(j);
            }
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;

        MiniGameManager.instance.isShuffle = false;
    }
}
