using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    // Hex 타일 반환
    public Vector3Int offsetCoordiantes;

    // 길찾기 알고리즘때 사용할 Queue
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

        // 유닛의 현재 위치를 받아와 스타트 지점을 정합니다
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

            // 스타트 지점부터 목표까지 이동
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

    // 타일의 위치를 받아옵니다
    public void SetOffset(Vector3Int tileOffset)
    {
        offsetCoordiantes = tileOffset;
    }

    // 현재 위치에 있는 타일을 반환합니다
    public Tile GetTile()
    {
        return tileGrid.GetTileAt(offsetCoordiantes);
    }
}
