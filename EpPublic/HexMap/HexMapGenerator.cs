using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject haxTileMap;

    [SerializeField] private float tileZOffset = 10;
    [SerializeField] private float tileXOffset = 10;

    int maxWidth = 8;
    int maxHeight = 28;

    public float height = 1;

    private List<GameObject> tiles = new List<GameObject>();
    
    void Start()
    {
        CreateHexTileMap();
    }


    private void CreateHexTileMap()
    {
        int index = 0;
        int centerIndex;

        int delTile = maxHeight / 4;

        bool check = false;

        // 열
        for (int x = 0; x <= maxWidth; x++)
        {
            // 행
            for (int z = 0; z <= maxHeight; z++)
            {
                // x가 마지막 줄일때 z가 홀수라면
                //if (!(x == maxWidth && z % 2 != 0))
                //{
                tiles.Add(Instantiate(haxTileMap));
                tiles[index].transform.parent = gameObject.transform;

                // 인덱스는 0부터 시작
                if (z % 2 == 0)
                {
                    // 짝수 열
                    tiles[index].transform.position = new Vector3(x * tileXOffset + x * tileXOffset / 2, height, z * tileZOffset / 2);
                }
                else
                {
                    // 홀수 열
                    tiles[index].transform.position = new Vector3((tileXOffset * (6 * x + 3)) / 4, height, z * tileZOffset / 2);
                    //tiles[index].transform.position = new Vector3(x * tileXOffset + x * tileXOffset / 2 + tileXOffset * 3 / 4, height, z * tileZOffset / 2); 수식 간략화
                }


                index++;
            //}
            }
            if(index - maxHeight > maxHeight * maxWidth / 2)
                check = true;

            if (!check)
            {
                for (int i = 0; i < delTile; i++)
                {
                    if (tiles[i + x * (maxHeight + 1)].activeSelf)
                        //tiles.Remove(tiles[i + x * (maxHeight + 1)]);

                    tiles[i + x * (maxHeight + 1)].SetActive(false);

                    if (tiles[(x + 1) * (maxHeight) - i + x].activeSelf)
                        tiles[(x + 1) * (maxHeight) - i + x].SetActive(false);
                }

                delTile -= 2;
            }
            else if (check)
            {
                delTile++;
                for (int i = 0; i < delTile; i++)
                {
                    if (tiles[i + x * (maxHeight + 1)].activeSelf)
                        tiles[i + x * (maxHeight + 1)].SetActive(false);

                    if (tiles[(x + 1) * (maxHeight) - i + x].activeSelf)
                        tiles[(x + 1) * (maxHeight) - i + x].SetActive(false);
                }

                delTile++;
            }

        }

        for(int i = index - 1; i > index - maxHeight; i--)
        {
            
            if (i % 2 == 1&& tiles[i].activeSelf)
                tiles[i].SetActive(false);
        }

        centerIndex = index / 2 + maxWidth - 1;

        tiles[centerIndex].transform.position += new Vector3(0, 2, 0);


        Debug.Log(centerIndex);
    }
}
