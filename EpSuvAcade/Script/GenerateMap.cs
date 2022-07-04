using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Transform grid;         // 캐릭터에 따라 움직일 맵
    public Transform character;    // 캐릭터

    private Vector3[] pos = { new Vector3(100, 100, 0), new Vector3(-100, 100, 0),  new Vector3(-100, -100, 0), new Vector3(100, -100, 0) 
                            , new Vector3(0, 100, 0),   new Vector3(0, -100, 0),    new Vector3(-100, 0, 0),    new Vector3(100, 0, 0)};

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            float angle = Mathf.Atan2(character.position.y - transform.position.y, character.position.x - transform.position.x) * Mathf.Rad2Deg;

            Debug.Log("각도는" + angle);

            if (AngleCheck(0, angle, 43.5f, 46.5f))             // 오른쪽, 위로 이동
            {
                grid.position += pos[0];
            }
            else if (AngleCheck(0, angle, 133.5f, 136.5f))      // 왼쪽, 위로 이동
            {
                grid.position += pos[1];
            }
            else if (AngleCheck(1, angle, -136.5f, -133.5f))    // 왼쪽, 아래로 이동
            {
                grid.position += pos[2];
            }
            else if (AngleCheck(1, angle, -46.5f, -43.5f))      // 오른쪽, 위로 이동
            {
                grid.position += pos[3];
            }

            else if (AngleCheck(2, angle, 45, 135))             // 위로 이동
            {
                grid.position += pos[4];
            }
            else if (AngleCheck(2, angle, -135, -45))           // 아래로 이동
            {
                grid.position += pos[5];
            }
            else if (AngleCheck(1, angle, 135, 180) || AngleCheck(0, angle, -180, -135)) // 왼쪽으로 이동
            {
                grid.position += pos[6];
            }
            else if (AngleCheck(2, angle, -45, 45))             // 오른쪽으로 이동
            {
                grid.position += pos[7];
            }
        }
    }

    private bool AngleCheck(int c, float angle, float min, float max)
    {
        switch (c)
        {
            case 0:
                if (min <= angle && angle < max)
                    return true;
                break;
            case 1:
                if (min < angle && angle <= max)
                    return true;
                break;
            case 2:
                if (min < angle && angle < max)
                    return true;
                break;
        }
        return false;
    }
}
