using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroBounding : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = GetComponent<Collision>().GetContact(0);
        Vector3 dir = collision.gameObject.transform.position - cp.point; // ���������������� ź��ġ �� ����
        collision.gameObject.GetComponent<Rigidbody>().AddForce((dir).normalized * 300f);
    }
}
