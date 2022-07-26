using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 FirstPoint;
    private Vector3 SecondPoint;
    private Vector3 EndPoint;

    public float xAngle = 0f;
    public float yAngle = 55f;

    private float xAngleTemp;
    private float yAngleTemp;

    public void OnBeginDrag(PointerEventData eventData)
    {
        FirstPoint = eventData.position;
        xAngleTemp = xAngle;
        yAngleTemp = yAngle;

        Debug.Log("첫 " + FirstPoint);

    }

    public void OnDrag(PointerEventData eventData)
    {
        SecondPoint = eventData.position;
        xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
        yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.

        //// 회전값을 40~85로 제한
        //if (yAngle < 40f)
        //    yAngle = 40f;
        //if (yAngle > 85f)
        //    yAngle = 85f;

        //Debug.Log("두 " + SecondPoint);

        //transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndPoint = eventData.position;

        Debug.Log(GetDistance(EndPoint, FirstPoint));
        Debug.Log(GetDirection(EndPoint, FirstPoint));

    }

    // 두 벡터 거리 구하기
    float GetDistance(Vector3 vec1, Vector3 vec2)
    {
        // 종점(x2, y2) - 시작점(x1, y1)
        float width = vec2.x - vec1.x;
        float height = vec2.y - vec1.y;

        // 거리(크기)의 스칼라값을 구하기 위해 피타고라스 정리 사용
        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

    // 두 벡터 방향 구하기
    int GetDirection(Vector3 vec1, Vector3 vec2)
    {
        // 0 : 좌, 1 : 우, 2 : 상, 3 : 하
        float width = vec2.x - vec1.x;
        float height = vec2.y - vec1.y;

        if (Mathf.Abs(width) > 200)
        {
            return (width > 0) ? 0 : 1;
        }
        else if (GameManager.btnNum)
        {
            if (Mathf.Abs(height) > 200)
                return (height < 0) ? 2 : 3;
        }

        return -1;
    }

    // 두 벡터 각도 구하기
    float GetAngle(Vector3 vec1, Vector3 vec2)
    {
        Vector2 offset = vec2 - vec1;

        return (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
    }
}