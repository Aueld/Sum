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

        // �ѹ� ���� �ö󰬴ٰ� �������� ���
        // Y�� 400���� �ø� �� OnComplete�� ���� ���� �� ���� DOTween�� ����
        cards[index].GetComponent<RectTransform>().DOAnchorPosY(100f, 0.2f, false).SetEase(Ease.InExpo).OnComplete(() =>
        {
            cards[index].GetComponent<RectTransform>().DOAnchorPos(CardPos[index], 0.2f, false).SetEase(Ease.OutExpo);
        });

        // �¿�θ� ���̴� ���
        //cards[index].GetComponent<RectTransform>().DOAnchorPos(CardPos[index], 0.2f, false).SetEase(Ease.OutExpo);

        if (!MiniGameManager.instance.isShuffle)
            cards[index].GetComponent<RectTransform>().DORotate(Vector3.zero, 1f).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                // ���� ���� �� ���� ��ư�� Ŭ���Ǿ� ī�� �����Ⱑ Ǯ���� ���� �����Դϴ�
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
            // �ӽ÷� ������ �Լ��Դϴ�.
            cards[index].GetComponent<GS_Card>().HideCount();

            // ī�� �޸��� �ʿ��մϴ�
            // ī�带 ������ ���� ���
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
