using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallSpinMove : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject spin;

    private float speed = 0.8f;

    private Vector3 vec_rot;

    int width;
    
    private void Start()
    {
        width = Screen.width;
        
    }

    private void Update()
    {
   
        spin.transform.Rotate(vec_rot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("fff");


        if (eventData.position.x > width / 2)
        {
            Debug.Log(eventData.position.x);

            vec_rot = Vector3.up * speed;
        }
        else
        {
            Debug.Log(eventData.position.x);

            vec_rot = Vector3.down * speed;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        vec_rot = Vector3.zero;
    }
}
