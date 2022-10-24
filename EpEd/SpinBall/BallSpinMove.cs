using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BallSpinMove : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject spin;
    
    private Vector3 vec_rot;
    private float speed = 3f;
    private int width;
    
    private void Start()
    {
        width = Screen.width;    
    }

    private void Update()
    {
        spin.transform.position += Vector3.forward * 0.1f;

        spin.transform.Rotate(vec_rot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.position.x > width / 2)
        {
            vec_rot = Vector3.up * speed;
        }
        else
        {
            vec_rot = Vector3.down * speed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        vec_rot = Vector3.zero;
    }
}
