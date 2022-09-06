using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomInOut : MonoBehaviour
{
    public float speed = 10.0f;

    private List<Camera> Cameras = new List<Camera>();

    private void Start()
    {
        // ������Ʈ �� ī�޶� ���� �˻�
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Cameras.Add(transform.GetChild(i).GetComponent<Camera>());
        }
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        if (scroll == 0)
            return;

        foreach (Camera camera in Cameras)
        {
            //�ִ� ����
            if (camera.fieldOfView <= 20.0f && scroll < 0)
            {
                camera.fieldOfView = 20.0f;
            }
            // �ִ� �� �ƿ�
            else if (camera.fieldOfView >= 40.0f && scroll > 0)
            {
                camera.fieldOfView = 40.0f;
            }
            // ���� �ƿ�.
            else
            {
                camera.fieldOfView += scroll;
            }
        }
    }
}