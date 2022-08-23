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
        private int radius = 8;             // ���� Ÿ���� ��ü ������
        [SerializeField]
        private float hexRadius = 0.05f;    // ���� Ÿ�ϸ� ������
        [SerializeField]
        private float height = 1;            // ���� Ÿ�� ����

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

            // Ÿ�� ���� ��
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 1; j <= i * 6; j++)
                {
                    TileGen(index);
                    index++;
                }
            }

            index = 1;

            // Ÿ�� ��ġ
            for (int i = 1; i <= radius; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    // ���� ���������� ����� 0, ���� �ݽð� �������� �ε��� ����
                    // 4 �϶� �ϴ�
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

        // Ÿ�� ��
        private void TileGen(int index)
        {
            tiles.Add(Instantiate(HexTile[tileCodes[index]]));
            tiles[index].transform.parent = gameObject.transform;

            tiles[index].transform.position = new Vector3(0, height, 0);
        }

        // Ÿ�� �̵�
        private void TileMove(Vector2 vec_hex, int rad, float y , GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius * rad;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius * rad;

            pos.transform.position = new Vector3(x, y, z);
        }

        // ���� Ÿ�ϸ��� ��ġ�� �޾ƿͼ� �̵�
        private void TileMoveRing(Vector2 vec_hex, int rad, float y, GameObject lastPos, GameObject pos)
        {
            float x = vec_hex.x * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.x;
            float z = vec_hex.y * Mathf.Sqrt(3) * hexRadius + lastPos.transform.position.z;

            pos.transform.position = new Vector3(x, y, z);
        }
    }
}