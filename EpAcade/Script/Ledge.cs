using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{

    #region Ledge variable

    public int blockCount;      // 블록 갯수
    public float blockSize;     // 블록 크기
    public int nowBlock;        // 현재 블록

    private Block[] blocks;

    private bool check;

    #endregion

    private void Start()
    {
        blocks = GetComponentsInChildren<Block>();
        Align();
    }

    // 블록 객체 나열
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

    // 블록 이동
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

    // 선택된 블록
    public void Select(int select)
    {
        if (!check)  // 블럭 이동 중 다시 눌리는것 방지
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
