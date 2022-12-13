using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class GS_CardBack : MonoBehaviour, IPointerClickHandler
{
    public GameObject Card;

    public void OnPointerClick(PointerEventData eventData)
    {
        // ���̴� ���߿� ���� ���� �ʰ� ����
        if (MiniGameManager.instance.isShuffle)
            return;

        // ī�� �������� ������
        Card.GetComponent<RectTransform>().DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
        gameObject.SetActive(false);
    }
}