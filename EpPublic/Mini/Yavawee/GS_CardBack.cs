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
        // 섞이는 도중엔 동작 하지 않게 적용
        if (MiniGameManager.instance.isShuffle)
            return;

        // 카드 정면으로 뒤집기
        Card.GetComponent<RectTransform>().DORotate(Vector3.zero, 0.4f).SetEase(Ease.OutExpo);
        gameObject.SetActive(false);
    }
}
