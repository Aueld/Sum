using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    // Hex Ÿ�� ��ȯ
    public Vector3Int offsetCoordiantes;

    // ��ã�� �˰��� ����� Queue
    private Queue<Vector3> pathPositions = new Queue<Vector3>();

    public Vector3Int HexCoords => offsetCoordiantes;
    public TileGrid tileGrid;
    public bool isMove = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    internal void MoveThrougPath(List<Vector3> currentPath)
    {
        List<Vector3> temp = currentPath;
        temp.Reverse();
        pathPositions = new Queue<Vector3>(temp);
        StartCoroutine(MoveQueue());
    }

    private IEnumerator Movement(Vector3 endPosition)
    {
        animator.SetBool("isWalk", true);
        isMove = true;

        // ������ ���� ��ġ�� �޾ƿ� ��ŸƮ ������ ���մϴ�
        Vector3 startPosition = transform.position;

        endPosition.y = startPosition.y;

        float timeElapsed = 0;

        while(timeElapsed < 0.5f)
        {
            timeElapsed += Time.deltaTime;
            float lerpStep = timeElapsed / 0.5f;

            if (startPosition.x - endPosition.x >= 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;

            // ��ŸƮ �������� ��ǥ���� �̵�
            transform.position = Vector3.Lerp(startPosition, endPosition, lerpStep);
            yield return null;
        }
        
        transform.position = endPosition;
        isMove = false;
        animator.SetBool("isWalk", false);
    }

    private IEnumerator MoveQueue()
    {
        while (pathPositions.Count > 0)
        {
            Vector3 target = pathPositions.Dequeue();

            yield return StartCoroutine(Movement(target));
        }
    }

    // Ÿ���� ��ġ�� �޾ƿɴϴ�
    public void SetOffset(Vector3Int tileOffset)
    {
        offsetCoordiantes = tileOffset;
    }

    // ���� ��ġ�� �ִ� Ÿ���� ��ȯ�մϴ�
    public Tile GetTile()
    {
        return tileGrid.GetTileAt(offsetCoordiantes);
    }
}
