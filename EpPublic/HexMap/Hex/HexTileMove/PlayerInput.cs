using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // 사용자 입력

    public UnityEvent<Vector3> PointerClick;

    private void Update()
    {
        DetectMouseClick();
    }

    private void DetectMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            PointerClick?.Invoke(mousePos);
        }
    }
}
