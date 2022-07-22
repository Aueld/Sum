using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{ 
    private static WaitForSeconds waitSec = new WaitForSeconds(1f);

    #region Block variable
    public Rigidbody[] blockRd;     // 블록 리지드바디
    public int type;                // 선택된 블록

    private int maxBlock;           // 최대 출력되는 블록

    private Vector3 forceVec;
    private Vector3 torqueVec; 

    private Ledge ledge;

    private bool result;
    #endregion

    private void Start()
    {
        maxBlock = GameManager.maxBlock;
        ledge = GetComponentInParent<Ledge>();
        Init();
    }

    private void LateUpdate()
    {
        if (transform.position.z > 2.5f)
            ReRoll();

        //if (transform.position.z > 2f)
        //    StartCoroutine(Hit());
        
    }

    private void ReRoll()
    {
        transform.Translate(0, 0, ledge.blockCount * ledge.blockSize * -1);
        Init();
    }

    // 초기화
    private void Init()
    {
        type = Random.Range(0, maxBlock);

        for (int i = 0; i < blockRd.Length; i++)
        {
            blockRd[i].gameObject.SetActive(type == i);

            StartCoroutine(InitPhysics());
        }
    }

    // 선택한 블록이 맞는지 체크
    public bool Check(int select)
    {
        result = (type == select);

        if (result)
        {
            blockRd[type].isKinematic = false;
            StartCoroutine(Hit());
        }
        
        return result;
    }

    // 물리, 위치 값 초기화
    private IEnumerator InitPhysics()
    {
        blockRd[type].isKinematic = true;
        yield return GameManager.wait;

        blockRd[type].velocity = Vector3.zero;
        blockRd[type].angularVelocity = Vector3.zero;
        yield return GameManager.wait;

        blockRd[type].transform.localPosition = Vector3.zero;
        blockRd[type].transform.localRotation = Quaternion.identity;

        yield return null;
    }

    // 블록 날리기
    private IEnumerator Hit()
    {
        blockRd[type].isKinematic = false;

        int rand = Random.Range(0, 2);

        forceVec = Vector3.zero;
        torqueVec = Vector3.zero;

        switch (rand)
        {
            case 0:
                forceVec = (Vector3.right + Vector3.up * 2f) * 3f;
                torqueVec = (Vector3.forward + Vector3.down) * 5f;
                blockRd[type].AddForce(forceVec, ForceMode.Impulse);
                blockRd[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;

            case 1:
                forceVec = (Vector3.left + Vector3.up * 2f) * 3f;
                torqueVec = (Vector3.back + Vector3.up) * 5f;
                blockRd[type].AddForce(forceVec, ForceMode.Impulse);
                blockRd[type].AddTorque(torqueVec, ForceMode.Impulse);
                break;

        }
        
        yield return waitSec;

        if (!blockRd[type].isKinematic)
        {
            blockRd[type].isKinematic = true;
        }
    }


}
 