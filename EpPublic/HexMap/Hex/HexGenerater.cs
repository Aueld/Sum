using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hex
{
    public class HexGenerater : MonoBehaviour
    {
        public GameObject HexTile;

        private List<GameObject> tiles = new List<GameObject>();
        private HexCoord hexagon;
        private int rad = 8;
        private float hexRadius = 0.05f;

        public float height = 1;

        private void Start()
        {
            hexagon = new HexCoord(0, 0);

            CreateHexTileMap(hexagon);
        }

        private void CreateHexTileMap(HexCoord hex)
        {
            TileGen(0);

            int index = 1;

            for (int i = 1; i <= rad; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    TileGen(index);
                    index++;
                }
            }

            index = 1;

            for (int i = 1; i <= rad; i++)
            {
                for (int j = 0; j < 6; j++)
                {
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

        private void TileGen(int index)
        {
            tiles.Add(Instantiate(HexTile));
            tiles[index].transform.parent = gameObject.transform;

            tiles[index].transform.position = new Vector3(0, height, 0);
        }

        private void TileMove(Vector2 vec, int r, float y , GameObject pos)
        {
            float x = vec.x * Mathf.Sqrt(3) * hexRadius * r;
            float z = vec.y * Mathf.Sqrt(3) * hexRadius * r;

            pos.transform.position = new Vector3(x, y, z);

        }

        // 이전 타일맵의 위치를 받아와서 설치함
        private void TileMoveRing(Vector2 vec, int r, float y, GameObject lastPos, GameObject pos)
        {
            float x = vec.x * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.x;
            float z = vec.y * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.z;

            pos.transform.position = new Vector3(x, y, z);

            if (r == 3)
            {
                pos.transform.position += new Vector3(0, 1, 0);
            }
        }
    }
}