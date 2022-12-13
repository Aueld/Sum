using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BounceBall : MonoBehaviour
{
    private bool trueBall;
    private float power;

    private Transform Tr;
    private Rigidbody rb;

    private MeshRenderer meshRend;

    private void Start()
    {
        power = 100f;
        Tr = transform;
        meshRend = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        StartForce();
    }

    public void SetTrueBall(bool check)
    {
        trueBall = check;
    }

    public void SetisKinematic(bool check)
    {
        rb.isKinematic = check;
    }

    public void SetPower(float power)
    {
        this.power = power;
    }

    public bool GetTrueBall()
    {
        return trueBall;
    }

    public MeshRenderer GetMaterial()
    {
        return meshRend;
    }

    public void StartForce()
    {
        rb.AddForce(Random.insideUnitCircle * power);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ExcecuteReBounding(collision);
    }

    private void ExcecuteReBounding(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        Vector3 dir = Tr.position - cp.point;
        rb.AddForce((dir).normalized * power);
    }
}