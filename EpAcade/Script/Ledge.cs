using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    public int blockCount;      // ��� ����
    public float blockSize;     // ��� ũ��
    public int nowBlock;        // ���� ���

    private Block[] blocks;

    private bool check;

    private void Start()
    {
        blocks = GetComponentsInChildren<Block>();
        Align();
    }

    // ��� ��ü ����
    private void Align()
    {
        blockCount = blocks.Length;

        if(blockCount == 0)
        {
            return;
        }

        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for(int i = 0; i < blockCount; i++)
        {
            blocks[i].transform.Translate(0, 0, i * blockSize * -1);
        }
    }

    // ��� �̵�
    private IEnumerator Move()
    {
        check = true;

        yield return GameManager.wait;

        float next = transform.position.z + 2;
        
        while (transform.position.z < next)
        {
            transform.Translate(0, 0, Time.deltaTime * 15f);
            yield return null;
        }

        transform.position = Vector3.forward * next;
        nowBlock = (nowBlock + 1) % blockCount;

        check = false;
    }

    // ���õ� ���
    public void Select(int select)
    {
        if (!check)  // �� �̵� �� �ٽ� �����°� ����
            if (blocks[nowBlock].Check(select))
            {
                StartCoroutine(Move());
                GameManager.Success();
            }
            else
            {
                GameManager.Fail();
            }

    }
}
