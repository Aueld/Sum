using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DmgText : MonoBehaviour
{
    private TextMeshPro tmp;
    private Color alpha;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        Invoke("DestroyObject", 2);
        alpha = new Color32(255, 50, 50, 255);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);
        tmp.color = alpha;
    }

    private void DestroyObject()
    {
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
