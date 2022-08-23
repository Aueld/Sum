using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex
{
    public class HexGenerater : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> HexTile;
        [SerializeField]
        private int radius = 8;             // 헥사곤 타일의 전체 반지름
        [SerializeField]
        private float hexRadius = 0.05f;    // 헥사곤 타일맵 반지름
        [SerializeField]
        private float height = 1;            // 헥사곤 타일 높이

        private List<GameObject> tiles = new List<GameObject>();
        private List<int> tileCodes = new List<int>();

        private HexCoord hexagon;
        private int tileIndex = 0;

        private void Start()
        {
            hexagon = new HexCoord(0, 0);

            CreateHexTileMap(hexagon);
        }

        private void CreateHexTileMap(HexCoord hex)
        {
            tileCodes.Add(0);

            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    //if (i == 4)
                    //    tileCodes.Add(1);
                    //else
                        tileCodes.Add(0);

                    //tileCodes.Add(Random.Range(0, 0));
                }
            }

            tileIndex = tileCodes.Count;

            TileGen(0);

            int index = 1;

            // 타일 생성 후
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    TileGen(index);
                    index++;
                }
            }

            index = 1;

            // 타일 배치
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    // 눕힌 육각형에서 상단이 0, 이후 반시계 방향으로 인덱스 증가
                    // 4 일때 하단
                    TileMove(hex.Corner(j + 4), i, height, tiles[index]);
                    index++;

                    for (int k = 1; k < i; k++)
                    {
                        TileMoveRing(hex.Corner(j), i, height, tiles[index - 1], tiles[index]);
                        index++;
                    }
                }
            }
        }

        // 타일 젠
        private void TileGen(int index)
        {
            tiles.Add(Instantiate(HexTile[tileCodes[index]]));
            tiles[index].transform.parent = gameObject.transform;

            tiles[index].transform.position = new Vector3(0, height, 0);
        }

        // 타일 이동
        private void TileMove(Vector2 vec_hex, int rad, float y , GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius * rad;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius * rad;

            pos.transform.position = new Vector3(x, y, z);
        }

        // 이전 타일맵의 위치를 받아와서 이동
        private void TileMoveRing(Vector2 vec_hex, int rad, float y, GameObject lastPos, GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.x;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.z;

            pos.transform.position = new Vector3(x, y, z);
        }
    }
}